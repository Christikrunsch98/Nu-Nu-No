INCLUDE ..\globals.ink

// Teamleiter: State 3 : ON
Hey, wieso antwortest du nicht auf meine Mail?

Was hast du die ganze Zeit gemacht?

Du kannst mir nicht erzählen, dass es keine Arbeit gibt!

*[Wenn sie so wichtig sind, wäre es vielleicht sinnvoll, deutlicher darauf hinzuweisen.] #rage:-2
    ->antwort0
*[Nein, Arbeit gibt es genug. Du brauchst mich nicht unter Druck setzen. Ich kenn unsere Prioritäten!] #rage:-3
    ->antwort1
*[Verstanden, ich werde mich sofort darum kümmern. Danke für die Erinnerung!] #rage:-3
    ->antwort2

=== antwort0 ===
Ich hoffe das ist dir deutlich genug. Also los! 
~ SwitchDialogueState("Off")
-> DONE

=== antwort1 ===
Ja, wir wollen hier keine Zeit verlieren! 
~ SwitchDialogueState("Off")
-> DONE

=== antwort2 ===
Das will ich hören! Na dann auf auf! 
~ SwitchDialogueState("Off")
-> DONE
