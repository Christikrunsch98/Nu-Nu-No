INCLUDE ..\globals.ink

// Chef: State 1 : OFF
Was gibt's?
-> main

=== main ===
Irgendetwas Gutes zu melden?!
    *[Ja, tatsächlich]
        ->ant0
        
    *[Nein]
        ->ant1
        
=== ant0 ===
Dann lass mal hören!
    *[Ihr Vortrag war gut]
        Das interessiert mich einen Scheissdreck! #rage:3
        -> DONE
    *[Ach nichts]
        Bist du hier um mir ein Ei ans Ohr zu labbern?! Ab an die Arbeit! #rage:2
        -> DONE

=== ant1 ===
Wie kannst du es wagen! Mach dich sofort an die Arbeit! #rage:1

-> DONE