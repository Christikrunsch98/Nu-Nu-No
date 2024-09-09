INCLUDE ..\globals.ink

// Kumpel: State 2 : OFF
Brauchst du etwas?

*[Die einen mehr, die anderen weniger!]
    ->antwort0

*[Ich mag deinen Optimismus!]
    ->antwort1

*[Bist du sicher? Unser Chef war schon ziemlich mies gelaunt...]
    ->antwort2

=== antwort0 ===
Jeder Tag ist ein Chance, neu und frisch zu starten! #rage:-1
~ SwitchDialogueState("Off")
-> DONE

=== antwort1 ===
Haha! Na klar! #rage:-1
~ SwitchDialogueState("Off")
-> DONE

=== antwort2 ===
Hach mein Freund, das passt schon! Nicht verzagen, lass dir von keinem den Tag verderben! #rage:-1
~ SwitchDialogueState("Off")
-> DONE
