Ajout d'un 

Game manager le grid manager doit uniquement servir à géner les tuiles 
le game manager pourra utiliser le grid manager fait

deux actions déplacement et attaque

coord x,y
Attaque (fonction attaque retourne une liste de Tile en fonction de la range)
la class player deviendra invisible et servira à selectionner les tiles (on click display Character.UI)fait


pour créer des Objets Instanciate Function

On dessine une map ,les cases de bases sont transparantes, les autres ont leurs propre sprite qui est redéssiné dessus.


Character en deux parties CharacterInWorld la position et l'interaction avec le terrain

                            CharacterCharacteristics contient la logique du personnage
Class invocateur


ListeStatique contenant toutes les equipes?->fait

Class level avec Instantiation d'un nouveau level

Chaque level contient un tableau de Tuile (si on ne change pas le background il suffit d'enlever tous les personnages vivants et de recreer un terrain)

List de CharacterCharacteristics pour stocker tous les personnages et leur etat à la fin d'un niveau->fait

Phase d'amélioration des personnages entre les niveaux

Si on veux changer le background, se renseigner comment on change de scene genre tous les 5 stages changement d'environement foret->lave->soulocean;

Scene ecran titre bouton jouer pour tenter lel

Differentes scene 

regarder comment créer une scene en script 

Mettre les TileMap en prefab ou en attributs static pour pouvoir les Instancier

ce qui me permettrai de designer autant de level que je veux 

Création d'une scene->création d'un level ->avec une map de base déja fabriquée ->ajout des murs aléatoirs...



Lorsque l'on lance un combat, ouverture d'une scene spécial 
jouer animation attaque du personnage
jouer animation recevoir du perso attaqué 
ferme l'ui et retour sur l'ui global

Pour placer les personnages petite UI sur le coté 
sorte de menu déroulant qui affiche la liste des personnages
que le joueur possède et possibilité de drag and drop

Option pour masquer tous les personnages -> créer une Interface pour les tiles (peut être reconceptioner les Tiles avec TilesInWorldEt TileCharacteristic) pour ajouter des spawn tiles et tout
Si on démasque les personnages -> Masquer L'ui des cases Bouton oeil barre d'actions générale afficher Ui du niveau (nombre d'enemies ,objectifs ...)
Déplacement de la caméra au clavier Zoom molette

Problemes actuels AU SECOUR JE TAPE DEUX FOIS -> parce que j'appelais deux fois la fonction (:



Mettre la création du terrain dans le awake
puis comme ça lors de la création du personnage-> on retrouve le tableau

Les animations fonctionnnt plutot bien (petit delai) -> réglé has exit Time -> settings



OPTION DE GAMEPLAY

Build Batiments (soit les batiments sont des personnages qui ont 0 points de mouvements ou nouvelle classe batiment qui occupe un tableau de Tile)


HealthBar-> Deux recrangle

ERREUR SI click sur un perso->attaque-> clique sur le même perso-> corigée


Déplacement smooth validé

le temps d'appel entre deux tiles doit être légerement superieur au temps d'appel de déplacement


Pour plus tard peut être passer en Isometric Tile en effectuant une rotation des tuiles de 45°
Modification de l'offset à chaque création.


UI de choix de personnages -> Bah c'est pas trop mal 
Amélioration -> faire en sorte qu'a la séléction le charactere séléctionné deviennent plus grand et occupe le centre de l'écran
-> Transition vers la scène suivante
Petit bug au niveau des tours aussi 


Les fonctions à coder pour avoir une boucle de gameplay jouable

StartWave1 -> on récupère les informations dans la liste des charactéristiques et on fait spawn nos personnages


NextWave-> on refait spawn des enemies.

Change Area -> On affiche le choix de nouvel allié on change de scene (de terrain et StartWave1)


S'occuper de l'IA des alliés 

on regarde ou ils peuvent se déplacer et la case à target est la case ou ils sont à portée d'un des personnages allié

Class Level avec attribut WaveRestantes?

Dessin des Tiles map

Poffiner les animations
Drag&Drop pour placer les unitées.

ET ON SERA PAS MAL
-> Système de Wave -> OK