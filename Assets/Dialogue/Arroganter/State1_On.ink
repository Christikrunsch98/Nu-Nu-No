INCLUDE ..\globals.ink

// Arroganter: State 1 : ON

Hey, ich bin nächste Woche im Urlaub, kannst du mein Projekt mit übernehmen? Hehe

*[Wer hier Erfolg haben will, zeigt Einsatz. Wenn das nicht dein Ding ist, überleg' dir deine Prioritäten.] #rage:0 // Einfaches Hinnehmen der Sache
->antwort0
*[Urlaub klingt toll! Ich bin nächste Woche voll ausgelastet, aber lass uns jemanden finden, der dich vertreten kann.] #rage:0 // Entgegensetzen
->antwort1


===antwort0===
Bleib Locker, weißt du, einer muss es ja machen. Dann halt nicht! #rage:-1 
~ SwitchDialogueState("Off")
-> DONE

===antwort1===
Wer soll das sonst machen? Ach, mach was du willst... #rage:-1 
~ SwitchDialogueState("Off")
-> DONE
