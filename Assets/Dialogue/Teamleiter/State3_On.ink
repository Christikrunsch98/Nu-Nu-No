INCLUDE ..\globals.ink

// Teamleiter: State 3 : ON
Hey, wieso antwortest du nicht auf meine Mail?

Was hast du die ganze Zeit gemacht?

Du kannst mir nicht erzählen, dass es keine Arbeit gibt!

*[Ich war so beschäftigt mit meinen Aufgaben, dass ich deine Mail wohl übersehen habe.]
    ->antwort0
*[Es tut mir leid, dass ich nicht schneller geantwortet habe. Danke, dass du mich darauf hingewiesen hast.]
    ->antwort1
*[Verstanden, ich werde mich sofort darum kümmern. Danke für die Erinnerung!]
    ->antwort2

=== antwort0 ===
Na dann aber jetzt zack zack. Jetzt bloß keine Zeit verschwenden! #rage:-1
~ SwitchDialogueState("Off")
-> DONE

=== antwort1 ===
Ja, wir wollen hier keine Zeit verlieren! #rage:-2
~ SwitchDialogueState("Off")
-> DONE

=== antwort2 ===
Das will ich hören! Na dann auf auf! #rage:-3
~ SwitchDialogueState("Off")
-> DONE
