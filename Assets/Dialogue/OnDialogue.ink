INCLUDE globals.ink

Magst Burger oder Hühnchen mehr?

Also ich mag beides.
-> main

=== main ===
Wusstest du dass auf Burger auch manchmal Hühnchen drauf ist?
-> two

=== two ===
Was möchtest du essen?
    * [Einen Burger]
        -> burger
    * [Ein Hühnchen]
        -> huhn

=== burger ===
Sehr gute Wahl! Du solltest vielleicht Burgermeister werden, Hahaha!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE

=== huhn ===
Sehr gute Wahl! Wer Broiler nicht ehrt, der sein Chicken nicht wert! Hahaha!
// C# -> People.DialogueState = CurrentDialogueState.Off;
~ SwitchDialogueState("Off")
-> DONE
