INCLUDE ..\globals.ink

// Nörgler: State 1 : ON
Das ist doch wieder typisch, dass du nicht an die Unterlagen gedacht hast.

*[Ich kann das jetzt auch nicht mehr ändern. Morgen bring ich sie mit.]
    -> antwort1

*[ Bist du Gott?]
    -> antwort2

*[ Aber ich habe sie doch mit. Hier.]
    -> antwort3

===antwort1===
Ja, sagen kannst du viel.

-> DONE
~ SwitchDialogueState("Off")
===antwort2===
Wie bitte?
~ SwitchDialogueState("Off")
-> DONE

===antwort3===
Ohh. Ah schön
~ SwitchDialogueState("Off")
-> DONE
