INCLUDE ..\globals.ink

// Sekräterin: State 0 : OFF
Hi hi, Hallo! Willkommen in unserem Office!

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
Ähm... nun ja. Ich frag einfach so. Ich dachte das wäre ein Witziges Spiel.
-> farbe

    
=== lila ===
OMG, echt?! Ohh wow, hi hi. Das nehme ich als Kompliment! 
    * [Ha ha, gerne] #rep:10
     
        Ohhhh!!!>.<'❤
        ->end
    * [Du bist ja eine Lustige] #rep:5
  
        Warte, warte! So war das nicht gemeint! 
        -> end

=== blau ===
Ah okay... Hmm, blau liegt in der Farbskala nah bei Lila... 

Ähm, Danke!
~ SwitchDialogueState("Off")
-> DONE

=== end ===
Jetzt bin ich ganz außer mir. Danke für das nette Gespräch!
~ SwitchDialogueState("Off")
-> DONE
    
