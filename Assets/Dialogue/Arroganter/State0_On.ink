INCLUDE ..\globals.ink

// Arroganter: State 0 : ON
Hey, ich bin nächste Woche im Urlaub, kannst du mein Projekt mit übernehmen? Hehe

*[Naja, okay.]  // Einfaches Hinnehmen der Sache
->antwort0
*[Was soll das? Das hättest du doch besser planen können.]  // Entgegensetzen
->antwort1


===antwort0===
Bleib Locker, weißt du, einer muss es ja machen. Ich hab dich gefragt, weil du der beste Arbeiter von uns bist, HeHe #rage:2
~ SwitchDialogueState("Off")
-> DONE

===antwort1===
Was heißt hier planen? Du machst das doch! Hehe, eingebildeter Schnösel! Und jetzt geh mir aus der Sonne du Gutmensch!  #rage:3
~ SwitchDialogueState("Off")
-> DONE
