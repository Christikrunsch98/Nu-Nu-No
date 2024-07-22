INCLUDE ..\globals.ink

// Sekräterin: State 0 : ON
Hi hi, Hallo! Ich hab dich gar nicht gesehen. Willkommen in unserem Office!

Darf ich dir eine Frage stellen?
->farbe

=== farbe ===
Wenn du dich hier umschaust, welche Farbe gefällt dir am meisten?
    * [Lila!] #rep:10
        -> lila 
    * [Blau] #rep:5
        -> blau 
    * [Äh... Wie bitte?] #rep:0
        -> komisch 
        
=== komisch ===
 #score:0
Ähm... nun ja. Ich frag einfach so. Ich dachte das wäre ein Witziges Spiel.
-> farbe

    
=== lila ===
 #score:2
OMG, echt?! Ohh wow, hi hi. Das nehme ich als Kompliment! 
    * [Ha ha, gerne] #rep:10
     #score:5
        Ohhhh!!!>.<'❤
        ->end
    * [Du bist ja eine Lustige] #rep:5
     #score:2
        Warte, warte! So war das nicht gemeint! 
        -> end

=== blau ===
 #score:1
Ah okay... Hmm, blau liegt in der Farbskala nah bei Lila... 

Ähm, Danke!
~ SwitchDialogueState("Off")
-> DONE

=== end ===
Jetzt bin ich ganz außer mir. Danke für das nette Gespräch!
~ SwitchDialogueState("Off")
-> DONE
    
