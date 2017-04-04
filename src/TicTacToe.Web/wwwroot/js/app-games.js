// app-examens.js
(function () {

    "use strict";

    // Creating the Module
    angular.module("app-games", ["simpleControls", "ngRoute", "ui.bootstrap", "ngMaterial", "ngMessages"])
      .config(function ($routeProvider) {

          $routeProvider.when("/", {
              controller: "gamesController",
              controllerAs: "vm",
              templateUrl: "/views/gamesView.html"
          });

          $routeProvider.when('/ouvrir/:game/:player', {
              controller: 'gameController',
              controllerAs: "vm",
              templateUrl: '/views/gameView.html'
          });

          $routeProvider.otherwise({ redirectTo: "/" });

      })
       .config(function ($mdDateLocaleProvider) {
           $mdDateLocaleProvider.formatDate = function (date) {
               return date.toLocaleDateString();
           };
       })
       .constant('Constants', {
           Token: {
               0: '_',
               1: 'X',
               2: 'O'
           },
           State: {
               0 : 'WaitingPlayerXMove',
               1 : 'WaitingPlayerOMove',
               2 : 'GridFull',
               3 : 'PlayerXWins',
               4 : 'PlayerOWins'
           },
           DisplayState: {
               0 : "Au joueur 'X'",
               1 : "Au joueur 'O'",
               2 : 'Égalité !',
               3 : "Le joueur 'X' a gagné !",
               4 : "Le joueur 'O' a gagné !"
           }
       });

})();