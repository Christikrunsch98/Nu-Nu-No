INCLUDE ..\globals.ink

// Arroganter: State 0 : ON
Moin
->main

===main===

Hey, ich bin nächste Woche im Urlaub, kannst du mein Projekt mit übernehmen? Hehe

*[Wer hier Erfolg haben will, zeigt Einsatz. Wenn das nicht dein Ding ist, überleg' dir deine Prioritäten.]

->antwort0
*[Urlaub klingt toll! Ich bin zwar nächste Woche voll ausgelastet, aber lass uns jemanden finden, der dich vertreten kann.]

->antwort


===antwort0===
Wie bitte??!!
->DONE

===antwort===
Du Schwein! Hehe
//~ ContinueToNextGameState("Next") 
-> DONE

