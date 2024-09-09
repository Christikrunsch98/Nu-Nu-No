INCLUDE ..\globals.ink

// Teamleiter: State 2 : ON
Hey, wieso antwortest du nicht auf meine Mail?

Was hast du die ganze Zeit gemacht?

Du kannst mir nicht erzählen, dass es keine Arbeit gibt!

*[Ich bin sofort dabei...']
    ->antwort0
*[Ich war so beschäftigt mit meinen Aufgaben, dass ich deine Mail wohl übersehen habe.]
    ->antwort1
*[Es gibt genug Arbeit ja, aber ich kann auch nicht alles auf einmal machen!]
    ->antwort2

=== antwort0 ===
Na dann aber ab! #rage:1
~ SwitchDialogueState("Off")
-> DONE

=== antwort1 ===
Hör mal! Dein Aufgabenbereich ist klar definitert!

Sollte ich nochmal so eine Nachläsigkeit von dir erfahren, wirst du bald mal bei unserem Chef vorsitzen! #rage:3
~ SwitchDialogueState("Off")
-> DONE

=== antwort2 ===
Was ist denn das für ein Ton? Du scheinst wohl zu vergessen, wer vor dir steht! #rage:3
~ SwitchDialogueState("Off")
-> DONE
