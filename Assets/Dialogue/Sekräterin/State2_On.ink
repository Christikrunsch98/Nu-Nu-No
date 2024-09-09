INCLUDE ..\globals.ink

// Sekräterin: State 2 : ON
Oh nein! Mein Schokokuchen ist geschmolzen!

Ich wollte ihn doch in den Kühlschrank stellen!>.<

*[Oh! Wie schade, das tut mir leid!]
    ->antwort0
*[Vielleicht können wir ja mal einen neuen backen.]
    ->antwort1
*[Unser Freund dahinter am Snackautomat würde den sicher noch essen...]
    ->antwort2
    
=== antwort0 ===
Oh danke für dein Mitgefühl!
~ SwitchDialogueState("Off")
-> DONE

=== antwort1 ===
Ohh! Wie lieb von dir! Das wäre so nett!
~ SwitchDialogueState("Off")
-> DONE

=== antwort2 ===
Mhmm... Stimmt, er würde ihn bestimmt noch essen...

Na gut!
~ SwitchDialogueState("Off")
-> DONE