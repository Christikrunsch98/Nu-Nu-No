INCLUDE ..\globals.ink

// Teamleiter: State 0 : ON
Hey, wieso antwortest du nicht auf meine Mail?

Was hast du die ganze Zeit gemacht?

Du kannst mir nicht erzählen, dass es keine Arbeit gibt!

*[Ich bin sofort dabei...']
    ->antwort0
*[Wenn sie so wichtig sind, wäre es vielleicht sinnvoll, deutlicher darauf hinzuweisen.]
    ->antwort1
*[Es gibt genug Arbeit ja, aber ich kann auch nicht alles auf einmal machen!]
    ->antwort2

=== antwort0 ===
Na dann aber ab!
-> DONE

=== antwort1 ===
Hör mal! Dein Aufgabenbereich ist klar definitert!

Sollte ich nochmal so eine nachlässige Antwort von dir bekommen, kannst du dich auf was gefasst machen!
-> DONE

=== antwort2 ===
Was ist denn das für ein Ton? Du scheinst wohl zu vergessen, wer vor dir steht!

-> DONE





