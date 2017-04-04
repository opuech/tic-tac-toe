// examensController.js
(function () {

    "use strict";
    angular.module("app-games").controller("gamesController", gamesController);

    function gamesController($http) {

        var vm = this;

        vm.games = [];

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/game/GetAllGamesInfos")
          .then(function (response) {
              // Success
              angular.copy(response.data, vm.games);
          }, function (error) {
              // Failure
              vm.errorMessage = "Failed to load data: " + error;
          })
          .finally(function () {
              vm.isBusy = false;
          });

        



    }

})();