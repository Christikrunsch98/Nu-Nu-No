INCLUDE ..\globals.ink

// Nörgler: State 1 : ON
Das ist doch wieder typisch, dass du nicht an die Unterlagen gedacht hast. #rage:2

*[Ich kann das jetzt auch nicht mehr ändern. Morgen bring ich sie mit.] #rage:0
    -> antwort1

*[ Bist du Gott?] #rage:-1
    -> antwort2

*[ Aber ich habe sie doch mit. Hier.]#rage:-2
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
