"use stricts";

var loginModule = function () {
    var _settings = {
        urlRequestAuthorizationToSpotify: ""
    };

    var _openSpotifyAuthorizationScreen = function () {
        var result = window.open(_settings.urlRequestAuthorizationToSpotify, "_blank ", "width=500,height=700");
        if (result && result.closed()) {
            location.reload();
        }
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

