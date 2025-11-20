VAR playerName = ""

You flip on the lights — flickering neon hums, dust drifting through the air. The counter still smells faintly of soy and smoke.

{playerName}: "It’s been a while… smells like dust and old broth."

You move behind the counter, brushing off dust, checking the stove. The broth pot is empty — you fill it, light the burner. Steam begins to rise.

Small clatter from behind the counter.

{playerName}: "...Who’s there?"

You see a kid, small, scrappy, wearing a rag too big for him. He freezes mid-step, caught reaching for the tip jar.

Aki: "Uh hey! I was just, uh, checking if you were… open! Yeah. I can pay! Maybe… without toppings."

-> aki_choice_1


=== aki_choice_1 ===

{playerName}: "How do I handle this kid?"

+ "... Sit down. I'll make you one, on me."
    #rel:Aki 1
    Aki (surprised): "You're… not gonna yell at me or tell me to leave?"
    {playerName}: "Just don’t do it again, okay?"
    Aki: "Okay! Can I get Shoyu Ramen with curly noodles?"
    -> begin_aki_cooking

+ "You steal from me again, you can eat the spoon instead."
    Aki: "Whoa okay, okay, no need to threaten cutlery violence."
    Aki: "I’ll pay. Promise. Just… don’t kick me out."
    -> begin_aki_cooking


=== begin_aki_cooking ===
order #order:Aki

-> aki_post_cook


=== aki_post_cook ===
#serve:Aki

Aki: *eats* "It's warm… it's good."


Aki: "You just opened, right? Don’t see many restaurants open this late."

{playerName}: "First night back."

Aki: "Back? Like, you ran this place before?"

{playerName}: "…"

Aki: *shrugs* "Guess it’s got that old smell."

He finishes eating, wipes his mouth on his sleeve.

Aki: "Thanks. Name’s Aki, by the way." 
Aki: *grins awkwardly* "I’ll… maybe pay you next time."

He stands up and turns to leave.

-> kaen_arrives


=== kaen_arrives ===

An older man steps in as Aki slips past him. Aki glances up, curious but unbothered, then darts out into the night.

Kaen: "Saw lights were on. Figured I’d see if it was real."

He looks around the shop, eyes tracing the walls like they hold memories.

Kaen: "Still smells like someone who remembers how to care for broth."

{playerName}: "You the health inspector or something?"

Kaen: *chuckles* "Not quite. Just a hungry old man with an empty stomach and a stubborn memory… You serving?"

{playerName}: *nods* "What do you want?"

Kaen: "Chef’s choice."

-> begin_kaen_cooking


=== begin_kaen_cooking ===
order #order:Kaen

-> kaen_post_cook


=== kaen_post_cook ===
#serve:Kaen

Kaen: "You’ve got steady hands. You cooked before."

{playerName}: "Maybe. Can’t remember much past leaving the city."

Kaen: "Mm. Happens. The city takes things. Memories first, heart next, if you let it."

He leans back as you set down the bowl.

Kaen: "Let’s see if your cooking’s still worth remembering."

He takes a long sip.

-> kaen_choice_2


=== kaen_choice_2 ===

+ "Be honest. I can take it."
    #rel:Kaen 1
    Kaen: *half-smiles* "You sure?"
    Kaen sets the chopsticks down slowly.
    Kaen: "It’s… good. Honest. You cook like someone who’s been away for a long time… but hasn’t forgotten everything. There’s a spark here, even if the memories are dim."
    Kaen: "Keep stirring like that… you might remember more than you think, someday."
    -> kaen_exit

+ "You don’t look like someone who eats street food often."
    Kaen: *chuckles* "Age makes you crave the simple things. The ones that don’t lie to you."
    He eats quietly, leaving half the bowl untouched.
    Kaen: "You’ve got the rhythm down. Just… missing the heart. Maybe it'll come back."
    -> kaen_exit


=== kaen_exit ===

Kaen: "Quiet place you’ve got here. City outside’s louder than it’s ever been."

He stands, leaving enough cash. He turns toward the door, pausing in the doorway.

Kaen: "Lock up early. Places like this attract ghosts before customers."


Kaen: *He nods, faint smile.* "See you around."

-> DONE
