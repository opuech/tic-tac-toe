TDD avec ASP.NET Core et AngularJS
=================

## Introduction

But du TDD : 
1. Garder un niveau de productivité, de confiance et de satisfaction constant malgré la complexité qui croit
2. Chasser les gaspillages par du contrôle qualité en continu :
    - Rectifier plus vite en cas de problème
    - Augmenter la confiance et la satisfaction
    - Réduire les engorgements en aval 
    - Livrer plus fréquement
    - Comprendre le fonctionnement de l'application plus facilement
    - Se Partager plus facilement le travail
    - Réduire la liste des développements presque terminés

Les risques du TDD :
1. Tests fragiles, couteux à maintenir
2. Trop de faux positif + faux négatifs 
3. Mayonnaise qui ne prend pas
4. Objectifs trop ambitieux puis abandon par manque de résultats

Les inconvénients du TDD :
1. Davantage de lignes de code avec le TDD 
2. Beaucoup de connaissances et beaucoup de réflexes à acquérir
3. Peu de personnes 'formées' en TDD 
4. Méthode exigeante
5. Méthode réputée extrème (sur-qualité)

Agenda
1.	Démonstration de l'application 'TicTacToe'
2.	Survol de l'Architecture
3.	Tests unitaires et d'acceptation : Concepts et exemples
4.	Développement d'une nouvelle exigence en Outside-in TDD

## Démonstration de l'application 'TicTacToe'
Go !

## Survol de l'Architecture
Asp.Net Core, AngularJS, SignalR

Principes d'architecture facilitant le TDD : 
1. Les développeurs sont des auteurs à succès !
    - Clean code
    - Abstraction et encapsulation 
    - Command and Query Separation
    - Programmation fonctionnelle : le monade Result et Rail road architecture
    
3. Assembler son code comme des lego !
    - Principes SOLID
    - Injection de dépendances
    - Composition root
    
4. Isoler le domaine
    - Domain Driven Design
    - Les différentes couches
    - Agrégats, Entité et 'ValueObject'
    - Repository pattern

    
## Tests unitaires et d'acceptation : Concepts et exemples
1. Tests d'acceptation
2. Triangulation
3. Tests unitaires
4. Moq

## Développement d'une nouvelle exigence en Outside-in TDD    
Exigence : 
-  Étant donnée une grille de jeu dans laquelle il ne reste qu'une case disponible et aucune chance de gagner, 
-  Quand le joueur sélectionne la case, 
-  Alors la partie est terminée et les deux joueurs sont à égalité 
