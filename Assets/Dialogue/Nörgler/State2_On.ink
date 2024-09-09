INCLUDE ..\globals.ink

// Nörgler: State 2 : ON
Warum wird hier nie auf meine Meinung gehört?!

*[Vielleicht solltes du deine Lautstärke etwas anpassen?]
    ->antwort1
    
*[Vielleicht solltest du mal ne neue Schallplatte aufsetzen?]
    ->antwort2
    

===antwort1===
Ich glaub nicht... Meinst du? SOLL ICH NOCH LAUTER REDEN?!
~ SwitchDialogueState("Off")
-> DONE

===antwort2===
Stimmt du hast recht! Nächstes Mal leg ich noch eins drauf! Zep Zep!
~ SwitchDialogueState("Off")
-> DONE