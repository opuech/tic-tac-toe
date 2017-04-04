// examenEditorController.js

(function () {
    "use strict";
    angular.module("app-games").controller("gameController", gameController);

    function gameController($routeParams, $http, $route, $location, $scope, $window, Constants) {
    
        var vm = this;

        vm.Constants = Constants;
        vm.playerName = $routeParams.player;
        vm.gameId = $routeParams.game;
        vm.errorMessage = "";
        vm.successMessage = "";
        vm.isBusy = true;
        vm.board = {};
        vm.hubStarted = false;
        var url;
        console.log($location.path());
        console.log($routeParams);

        vm.RefreshGame = function (newGame) {
            angular.copy(newGame, vm.board);
            console.log("RefreshGame", vm.board);
            $scope.$apply();
        }

        vm.MakeMove = function (colId, rowId) {

            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post('/Game/ApplyPlayerSelectionOnGame', { 'gameId': vm.gameId, 'nomPlayer': vm.playerName, 'line': rowId, 'column': colId })
              .then(function (response) {
                  // success        
                  angular.copy(response.data, vm.board);
                  vm.successMessage = "";
                  vm.errorMessage = "";

              }, function (error) {
                  // failure
                  console.log(error);
                  vm.errorMessage = error.data;
                  vm.successMessage = "";
              })
              .finally(function () {
                  vm.isBusy = false;
              });

        }

        url = "/game/GetBoardById?gameId=" + vm.gameId;
        $http.get(url)
          .then(function (response) {
              // success
              console.log("ResponseData", response.data);
              angular.copy(response.data, vm.board);

          }, function (err) {
              // failure
              console.log(err);
              vm.errorMessage = "Failed to load" + err;
              vm.successMessage = "";
          })
          .finally(function () {
              vm.isBusy = false;
          });

        var hub = $.connection.gameHub;
        hub.client.publishPost = vm.RefreshGame;
        $.connection.hub.logging = true;
        $.connection.hub.start().done(function () {
            hub.server.joinGame(vm.gameId);
            vm.hubStarted = true;
        });
    }


})();