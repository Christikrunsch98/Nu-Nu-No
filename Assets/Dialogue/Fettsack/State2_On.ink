INCLUDE ..\globals.ink

// Fettsack: State 2 : ON

Ich hab gehört, dass du gestern Überstunden machen musstest und es deswegen nicht zum Fußball Finale geschafft hast

Das tut mir jetzt aber Leid! 

Aber wer nicht mitreden kann, geht arbeiten so einfach ist!

    *[Das stimmt, wie ist es denn ausgegangen?] #rage:0 #rep:2    
        -> weiter
    *[Was glaubst du wer hier die Regeln aufstellt? Idiot!]#rage:1 #rep:-2
        -> beleidigung
    
=== weiter ===
Wie jetzt?! Das fragst Du noch? Was ein Looser, HAHAHA! #rage:4 #rep:0
-> END

=== beleidigung ===
HEHE! Hör mal! Ich zeig Dich gleich beim Chef an mein Freund!
*[Bitteschön mach doch!] #rage:2 #rep:-2
    Beim nächsten mal gerne. Jetzt lass mich in Ruhe essen!
    -> DONE
    
*[Dafür müsstest du erstmal in sein Büro rollen, du Fettsack!] #rage:1 #rep:-5
    Pass auf Du erlebst gleich dein blaues Wunder!
    -> DONE
