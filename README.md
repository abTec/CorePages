Nasadene tu: http://194.182.82.20/

# Introduction 
Pri rieseni som si dovolil pouzit postup odporucany MSDN a to je EF + Razor Pages. MVC vsetci notoricky pozname a nic nove zaujimave by sme tam neveli. Razor Pages mi pripominaju Component Based Architecture podobnu ako v Angular alebo React. Zaroven som sa vsak do tejto architektury snazil napchat nejaku ine patterny pre potreby "interview test" kodu. V povodnej architekture by som asi mal datacontext pouzivat priamo v kontrolke. Cisty cas prace (bohuzial iba len) 7 hodin. Tracking historie: GIT

# Features
* Nacitanie suboru
* Pocitanie hashu suboru a zakazanie pridania rovnakeho suboru
* Ciastocna kontrola spravnosti formatu
* Vytvorenie databazy
* Analyza suboru - vytvorenie dat pre prechadzanie grafom
* Zobrazenie statistik pre kazdy set
* Nahodne generovanie mien pre ID userov
* Vykreslenie kompletneho grafu
* Klikatelne hrany vykreslia len susedov
* JSON data ako reprezentacia grafu

# How To
* Kedze su data sety readonly. Analyza poctu userov a dlzka ich friendlistu sa vyplni pred vloznim do db
* Po vytvoreni noveho data setu je moznost menit jeho meno
* Pri zobrazeni detailov sa vykresli kompletny graf
* Jednotlive nodes su klikatelne, vykresli sa potom len strom daneho node
* Do povodneho stavu sa graf uvedie klikom na Reset Graph
* K dispozicii je struktura grafu v Json formate

# Co by som vylepsil
* Samotne spracovanie suboru mi pride tazkopadne
* Pokrytie co najviac kodu testami
* Chcel by som doriesit ulohu prechadzania grafom (do sirky)
* Lepsie kreslenie velkych (1k+ nodes) grafov
* Pouzite AutoMapper a oddelenie domenovych tried od clienta
* Pouzite Moq v testoch
