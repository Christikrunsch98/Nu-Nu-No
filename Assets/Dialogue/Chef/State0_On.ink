INCLUDE ..\globals.ink

// Chef: State 0 : ON
An die Arbeit!
*[Ja okay] 
    ->ant0
    
*[Wie bitte?] 
    ->ant1
    
=== ant0 ===
Ich sag's nicht noch einmal! #rage:1
~ SwitchDialogueState("Off")
-> DONE

=== ant1 ===
Wie bitte?! Weißt du wen du vor dir hast?

Noch ein Wort! Ich sag's nicht noch einmal! #rage:3
~ SwitchDialogueState("Off")
-> DONE
