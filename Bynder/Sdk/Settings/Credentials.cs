﻿// Copyright (c) Bynder. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using Bynder.Sdk.Model;

namespace Bynder.Sdk.Settings
{
    /// <summary>
    /// Credentials implementation.
    /// </summary>
    internal class Credentials : ICredentials
    {
        private Token _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Credentials"/> class with a OAuth token.
        /// </summary>
        /// <param name="token">OAuth token passed in the configuration.</param>
        public Credentials(Token token)
        {
            _token = token;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Credentials"/> class with a permanent token.
        /// </summary>
        /// <param name="permanentToken">Permanent token passed in the configuration.</param>
        public Credentials(string permanentToken)
        {
            PermanentToken = permanentToken;
        }

        /// <summary>
        /// Check <see cref="t:ICredentials"/>.
        /// </summary>
        public event EventHandler<Token> OnCredentialsChanged;

        /// <summary>
        /// Check <see cref="t:ICredentials"/>.
        /// </summary>
        /// <value>Check <see cref="t:ICredentials"/>.</value>
        public string AccessToken => PermanentToken ?? _token?.AccessToken;

        /// <summary>
        /// Check <see cref="t:ICredentials"/>.
        /// </summary>
        /// <value>Check <see cref="t:ICredentials"/>.</value>
        public string RefreshToken => _token?.RefreshToken;

        /// <summary>
        /// Check <see cref="t:ICredentials"/>.
        /// </summary>
        /// <value>Check <see cref="t:ICredentials"/>.</value>
        public bool CanRefresh
        {
            get
            {
                return RefreshToken != null;
            }
        }

        /// <summary>
        /// Check <see cref="t:ICredentials"/>.
        /// </summary>
        /// <value>Check <see cref="t:ICredentials"/>.</value>
        public string TokenType => _token?.TokenType ?? "Bearer";

        /// <summary>
        /// Gets or sets the token that will be used to authenticate API calls.
        /// </summary>
        /// <value>The token.</value>
        private Token Token
        {
            get
            {
                return _token;
            }

            set
            {
                if (value != _token)
                {
                    _token = value;
                    OnCredentialsChanged?.Invoke(this, _token);
                }
            }
        }

        private string PermanentToken { get; set; }

        /// <summary>
        /// Check <see cref="t:ICredentials"/>.
        /// </summary>
        /// <returns>Check <see cref="t:ICredentials"/>.</returns>
        public bool AreValid()
        {
            if (PermanentToken != null)
            {
                return true;
            }

            if (_token != null
                && _token.AccessToken != null)
            {
                var limitExpiration = DateTimeOffset.UtcNow.AddSeconds(15);
                return _token.GetAccessTokenExpiration() > limitExpiration;
            }

            return false;
        }

        /// <summary>
        /// Check <see cref="t:ICredentials"/>.
        /// </summary>
        /// <param name="token">Check <see cref="t:ICredentials"/>.</param>
        public void Update(Token token)
        {
            if (Token == null)
            {
                Token = token;
            }
            else
            {
                token.RefreshToken = Token.RefreshToken;
                Token = token;
            }
            
        }
    }
}
