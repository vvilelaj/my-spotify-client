"use stricts";

var loginModule = function () {
    var _settings = {
        urlRequestAuthorizationToSpotify: ""
    };

    var _openSpotifyAuthorizationScreen = function () {
        window.open(_settings.urlRequestAuthorizationToSpotify, "_blank ", "width=400,height=400");
        return false;
    };

    var _init = function(settings) {
        _settings.urlRequestAuthorizationToSpotify =
            settings.urlRequestAuthorizationToSpotify || _settings.urlRequestAuthorizationToSpotify;
    };

    return {
        init : _init,
        openSpotifyAuthorizationScreen: _openSpotifyAuthorizationScreen
    };
}();

