INCLUDE ..\globals.ink

// Nörgler: State 0 : ON
Es ist ja immer dasselbe hier... Schon wieder ist mein Mittagessen verschimmelt
* [Vielleicht solltest du dein Lunchbox öfter wechseln]
-> antwort0
* [Hast du versucht, es als neuen Superfood-Trend zu verkaufen?]
->antwort1


===antwort0===
Mama hat das früher auch immer zu mir gesagt. Hmm... vielleicht ist ja was dran.
~ SwitchDialogueState("Off")
-> DONE

===antwort1===
Das ist nicht lustig. Ich mag keinen Schimmel auf meinem Essen!"
~ SwitchDialogueState("Off")
-> DONE

