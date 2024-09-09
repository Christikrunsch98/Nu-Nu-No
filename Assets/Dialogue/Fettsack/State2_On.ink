INCLUDE ..\globals.ink

// Fettsack: State 2 : ON

Ich hab gehört, dass du gestern Überstunden machen musstest und es deswegen nicht zum Fußball Finale geschafft hast

Das tut mir jetzt aber Leid! 

Aber wer nicht mitreden kann, geht arbeiten so einfach ist!

    *[Das stimmt, wie ist es denn ausgegangen?] #rage:0  
        -> weiter
    *[Was glaubst du wer hier die Regeln aufstellt?!]#rage:3
        -> beleidigung
    
=== weiter ===
Wie jetzt?! Das fragst Du noch? Was ein Looser, HAHAHA! #rage:4
~ SwitchDialogueState("Off")
-> END

=== beleidigung ===
HEHE! Hör mal! Ich zeig Dich gleich beim Chef an mein Freund!
*[Bitteschön mach doch!]  
    Warte nur ab! Und jetzt geh weiter Schuften du Gutmensch! #rage:2
    ~ SwitchDialogueState("Off")
    -> DONE
    
*[Dafür müsstest du erstmal in sein Büro rollen, du Fettsack!] #rage:3
    Pass auf Du erlebst gleich dein blaues Wunder!
    ~ SwitchDialogueState("Off")
    -> DONE
