# Changelog

## 1.0.7

### Fix

* [Graphics] Units are now properly displayed in front of the bushes
* [Performance] pointless computation on ressources have been reduced 
* [Gameplay] An issue causing bush to be not properly removed close by building
* [Gameplay] An issue allowing player to collect empty bushes, and more ressources than supposed to on all ressources
* [AI] Properly cleanup dead enemies and tasks in memory (fix crash when killing idle enemy)

## 1.0.6 94f05bc943746ac379ab53807fc84b355e546a98

### Fix

* [AI] Fix robot AI not clearing its task properly and spamming the pathfinder (causing heavy lag on late-game)

## 1.0.5 d29da768adaf24506f614dc21e08f9e15d855a04

### Fix

* [Gameplay] Lock the camera high enough to prevent player from killing idle robots (causing a crash)

## 1.0.4 66d1617e9cb98e05523cef000a1a851a596bcb5a

### Fix

* [Engine] Heavily reduced computation and memory cost of pathFinding

## 1.0.3 310345ef2430a833e852a06a7d6ebe5a174dae3e

### Fix

* [Graphics] Enemy are now properly displayed on the top half side of the map
* [Gameplay] When a building is destroyed, the tiles are now marked as empty
* [Gameplay] Reduced/Changed ressource hitbox to prevent unit being stuck behind them

## 1.0.2 e20e9867438fe973b530733d7f0c0c495840e4b3

### Help

We've given more informations in the tutorial text to help player understand what to do.

### Balancing

* Ressources :
  * In-Game ressources (Tree,BerryBush,Stone) now contains 10 less ressources
  * Worker now gather ressources 10 times slower
  * Most building-cost have been heavily reduced

* Ruin :
  * HP from 0 to 2500
  * Reduced time from 300 to 60 (you need 10 worker to achieve this speed)
* Bridge :
  * Wood cost from 400 to 40
* MainHouse :
  * HP from 30 to 300
  * Wood cost from 80 to 10
  * Food cost from 10 to 2
  * Stone cost from 20 to 20
* Workshop :
  * HP from 30 to 300
  * Wood cost from 80 to 15
  * Stone cost from 80 to 5
* WareHouse :
  * HP from 20 to 200
  * Wood cost from 100 to 20
  * Stone cost from 20 to 10
* StoneWall :
  * HP from 100 to 1000
  * Wood cost from 20 to 2
  * Stone cost from 30 to 8
* WoodWall :
  * HP from 40 to 400
  * Wood cost from 20 to 8
* Citizen :
  * HP from 30 to 200
  * Food cost from 100 to 10
  * Build time from 30s to 20s
* Spear :
  * Wood cost from 15 to 5
  * Build time from 30s to 10s
* Enemy :
  * Attack start at 150s instead of 300s
  * Spawn timer from 40s to 30s
  * HP from 10 to 100

### Fix

* [Sounds] not taking the configured volume (always at 100%)
* [Gameplay] Building production not consumming its ressources
* [Gameplay] When a ressource is removed from the map, the tile is now marked as empty
* [Gameplay] Enemy properly spawns now (up to 30, the spawn event was not properly executed)

## 1.0.1 42ba216cd6168ab156b4fdb15c9d986feb0e3610

### Feature

1. [Gameplay] Some building (House/WareHouse) have a different asset

## 1.0.0 5b7afc60da2615420c3200e1f4217606c42de84d

First Ludum-Dare release (1 hour before the end)
