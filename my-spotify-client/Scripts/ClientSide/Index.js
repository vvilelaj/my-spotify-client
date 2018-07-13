var helpersModule = (function () {
    var _isNullOrWhiteSpace = function (value) {
        return (typeof value !== "undefined" && value !== null && $.trim(value) !== "");
    };
    return {
        isNullOrWhiteSpace : _isNullOrWhiteSpace
    };
}());

var localStorageModule = (function () {
    "use strict";
    var _localStorage = window.localStorage;

    var _getState = function () {
        return localStorage.getItem("state");
    };

    var _setState = function (value) {
        localStorage.setItem("state", value);
    };

    var _geta = function () {
        return localStorage.getItem("state");
    };

    var _setState = function (value) {
        localStorage.setItem("state", value);
    };

    return {
        getState: _getState,
        setState:_setState
    };
}());

var loginModule = (function () {
    "use strict";
    //
    var _clientId = "239a234946e146b7be9a6f4733a3884d";
    var _accountBaseAddress = "https://accounts.spotify.com";
    var _authorizeEndPoint = "/authorize";
    var _redirectUri = "http://localhost:58604/ClientSide/Index";
    //
    var _settings = {
        hash: ""
    };

    var _login = function () {
        var state = new Date().getTime();
        localStorageModule.setState(state);
        var scope = "user-read-private user-read-email";
        var url = _accountBaseAddress + _authorizeEndPoint + "?" +
            "client_id=" + _clientId + "&" +
            "response_type=" + "token" + "&" +
            "redirect_uri=" + _redirectUri + "&" +
            "state=" + state + "&" +
            "scope=" + scope;
        location.href = url;
    };

    var _spotifyQueryStringAuthParameters = {
        accesToken : "access_token",
        tokenType : "token_type",
        expiresIn : "expires_in",
        state : "state"
    };

    var _authToken = {
        accesToken: "",
        tokenType: "",
        expiresIn: 0,
        created:0
    };

    var _hashStringHasAllParametersIn = function () {
        var isValid = true;

        for (var parameter in _spotifyQueryStringAuthParameters) {
            isValid = isValid && _settings.hash.index(_spotifyQueryStringAuthParameters[parameter]) >= 0;
        }

        return isValid;
    };

    var _init = function (hash) {
        if (!helpersModule.isNullOrWhiteSpace(hash)) return false;
        //_settings.hash = hash.substr(1);

        //if (_hashStringHasAllParametersIn()) {
        //    var parameterStrings = _settings.hash.split("&");
        //    for (var str in parameterStrings) {
        //        var param = str.split("=");
        //        _authToken[param[0]] = param[1];
        //    }
        //}
        
        return false;
    };
    

    return {
        init : _init,
        login : _login
    };
}());