INCLUDE ..\globals.ink

// Sekräterin: State 1 : ON
Ich habe neulich eine interessante Dokumentation über gesunde Ernährung gesehen.

Sie war wirklich sehr interessant. Wenn nur jeder das wüsste!

*[Erzähl mir mehr...]
    ->antwort1

*[Ja? Was war so gut daran?]
    ->antwort1

=== antwort1 ===
Es war besonders interessant zu sehen, wie Ernährung die mentale Gesundheit beeinflusst.

Kleine Änderungen können große Unterschiede machen!
~ SwitchDialogueState("Off")
-> DONE
