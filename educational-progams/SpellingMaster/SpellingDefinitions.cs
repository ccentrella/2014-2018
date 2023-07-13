using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellingMaster
{
	static class SpellingDefinitions
	{
		public static string BeginningWords =
		"The : The cat went away." + Environment.NewLine
		+ "An : Give me an apple." + Environment.NewLine
		+ "Cat : The cat is nice." + Environment.NewLine
		+ "Hat : I like the hat." + Environment.NewLine
		+ "Flat : The table is flat." + Environment.NewLine
		+ "Pat : I have a friend named Pat." + Environment.NewLine
		+ "Matt : Matt is nice." + Environment.NewLine
		+ "Mat : I purchased a mat." + Environment.NewLine
		+ "Rat : We saw a rat near our house." + Environment.NewLine
		+ "Nat : Nat is one of my best friends." + Environment.NewLine
		+ "Bat : I swung the bat." + Environment.NewLine
		+ "Vat : The vat held much liquid." + Environment.NewLine
		+ "Fat : I have some fat dogs." + Environment.NewLine
		+ "Ant : We bought ant traps." + Environment.NewLine
		+ "Gnat : I have been bitten by a gnat." + Environment.NewLine
		+ "Aunt : I love my aunt." + Environment.NewLine
		+ "Uncle : My uncle is very nice." + Environment.NewLine
		+ "They : They gave us many letters." + Environment.NewLine
		+ "May : May they help us?" + Environment.NewLine
		+ "Say : I always say nice things to my mom." + Environment.NewLine
		+ "Bay : We sailed across the bay." + Environment.NewLine
		+ "Lay : Do not lay on the floor." + Environment.NewLine
		+ "Day : It's a beautiful day." + Environment.NewLine
		+ "Pay : Always pay your debts." + Environment.NewLine
		+ "Ray : I love Ray." + Environment.NewLine
		+ "Them : Give them the candy." + Environment.NewLine
		+ "Sin : We all sin." + Environment.NewLine
		+ "Kin : He's one of my kin." + Environment.NewLine
		+ "Pop : Did you watch the balloon pop?" + Environment.NewLine
		+ "Rock : The big rock dropped." + Environment.NewLine
		+ "Top : I lost the top." + Environment.NewLine
		+ "Soap : Do not play with soap." + Environment.NewLine
		+ "Sop : Working in the rain can sop your clothes." + Environment.NewLine
		+ "Mop : Give me the mop." + Environment.NewLine
		+ "Cop : The cop gave me a speeding ticket." + Environment.NewLine
		+ "Flop : The new program was a complete flop." + Environment.NewLine
		+ "Sock : Please mend my sock!" + Environment.NewLine
		+ "Tree : That tree is huge!" + Environment.NewLine
		+ "Sky : The sky is gray." + Environment.NewLine
		+ "Mend : My mother can mend everything." + Environment.NewLine
		+ "Hop : Can you hop high into the air?" + Environment.NewLine
		+ "Mock : We should not mock others." + Environment.NewLine
		+ "Knock : Please knock at the door." + Environment.NewLine
		+ "Talk : Did you talk with your friends?" + Environment.NewLine
		+ "Lock : Did you lock the shed?" + Environment.NewLine
		+ "Dock : Did all the ships dock." + Environment.NewLine
		+ "Pocket : I reached in my pocket." + Environment.NewLine
		+ "Rocket : Look at the rocket!" + Environment.NewLine
		+ "Socket : The socket is out of place." + Environment.NewLine
		+ "Locket : Give me the locket." + Environment.NewLine
		+ "Happy : We are happy with our new house." + Environment.NewLine
		+ "Peppy : My younger brother is very peppy." + Environment.NewLine
		+ "Said : He said he did not know." + Environment.NewLine
		+ "Cake : Can you bake the cake?" + Environment.NewLine
		+ "Bake : Sally, bake the bread." + Environment.NewLine
		+ "Sake : Jesus died for our sake." + Environment.NewLine
		+ "Rake : Jimmy, rake the leaves." + Environment.NewLine
		+ "Snake : Watch out for the snake!" + Environment.NewLine
		+ "Take : Never take what does not belong to you." + Environment.NewLine
		+ "Make : Can you make ice cream?" + Environment.NewLine
		+ "Naked : Adam and Eve were naked before God supplied them with clothing." + Environment.NewLine
		+ "Sail : He set sail a while ago." + Environment.NewLine
		+ "Bail : The bail was deposited by his friend." + Environment.NewLine
		+ "Mail : Susan, give me the mail." + Environment.NewLine
		+ "Sale : The supermarket has many things on sale." + Environment.NewLine
		+ "Male : I saw the male bear." + Environment.NewLine
		+ "Fail : Do not fail your test again!" + Environment.NewLine
		+ "Lake : The beaver crossed the lake." + Environment.NewLine
		+ "Peak : Could you ever climb up to the peak?" + Environment.NewLine
		+ "Fly : Some birds fly beautifully." + Environment.NewLine
		+ "Soar : Birds can soar high into the sky." + Environment.NewLine
		+ "Lie : Always tell the truth. Never lie." + Environment.NewLine
		+ "Nice : The man is nice." + Environment.NewLine
		+ "Mice : Mice are cute." + Environment.NewLine
		+ "Rice : Rice is not very expensive." + Environment.NewLine
		+ "Vice : He overcame that vice." + Environment.NewLine
		+ "Lice : Be sure to keep away from lice." + Environment.NewLine
		+ "Dice : Roll the dice." + Environment.NewLine
		+ "Train : The train is stopped." + Environment.NewLine
		+ "Main : I like the main fountain." + Environment.NewLine
		+ "Vain : His searches were in vain." + Environment.NewLine
		+ "Cain : Cain was jealous of his brother, Abel." + Environment.NewLine
		+ "Cane : Do you need help with the cane?" + Environment.NewLine
		+ "Lane : Stay in the right lane!" + Environment.NewLine
		+ "Pane : The window pane is dirty." + Environment.NewLine
		+ "Pain : I have a pain in my side." + Environment.NewLine
		+ "Rain : It is beginning to rain." + Environment.NewLine
		+ "Insane : Do not act like you're insane." + Environment.NewLine
		+ "Right : Be sure you can tell what is right and wrong." + Environment.NewLine
		+ "Meat : Be sure to cook the meat well." + Environment.NewLine
		+ "Meet : I'm going to meet the president." + Environment.NewLine
		+ "Tame : Some dogs are tame, but others are vicious." + Environment.NewLine
		+ "Tape : I bought some tape." + Environment.NewLine
		+ "Cape : I wear a cape when it rains." + Environment.NewLine
		+ "Nap : My youngest son is going down for a nap." + Environment.NewLine
		+ "Map : The map will help you find your way." + Environment.NewLine
		+ "Tap : Tap the screen to access your files." + Environment.NewLine
		+ "Cap : Don't forget to replace the cap." + Environment.NewLine
		+ "Zap : I can quickly zap the bugs." + Environment.NewLine
		+ "Lap : Magdalene, sit on my lap." + Environment.NewLine
		+ "Gap : There's a huge gap in the wall." + Environment.NewLine
		+ "Sap : That tree produces sap." + Environment.NewLine
		+ "Rap : Jim, rap at the door." + Environment.NewLine
		+ "Wall : The wall is beautiful." + Environment.NewLine
		+ "Tall : The man is very tall." + Environment.NewLine
		+ "Saw : I saw the ocean." + Environment.NewLine
		+ "Paw : I received a cut from my cat's paw." + Environment.NewLine
		+ "Huge : The plane is huge!" + Environment.NewLine
		+ "Deluge : After the great deluge, the world was different." + Environment.NewLine
		+ "Rude : Be sure not to be rude." + Environment.NewLine
		+ "Crude : They used crude pieces of wood to make that dresser." + Environment.NewLine
		+ "Cruel : Don't be cruel to animals." + Environment.NewLine
		+ "Mute : Mute your speakers!" + Environment.NewLine
		+ "Toot : Did the bird toot a sweet song?" + Environment.NewLine
		+ "Sweet : The chocolate is very sweet." + Environment.NewLine
		+ "Sweat : When one exercises, they can begin to sweat." + Environment.NewLine
		+ "Sugar : Sugar is very sweet." + Environment.NewLine
		+ "Flip : Don't flip upside down." + Environment.NewLine
		+ "Down : The car crashed down into the canyon." + Environment.NewLine
		+ "Up : Look at the plane go up and up!" + Environment.NewLine
		+ "Town : We live in a nice town." + Environment.NewLine
		+ "Mound : Can you make a huge mound?" + Environment.NewLine
		+ "Hound : That hound is a very big dog!" + Environment.NewLine
		+ "Test : Be sure to take your test!" + Environment.NewLine
		+ "King : Jesus is the king of kings." + Environment.NewLine
		+ "Queen : A queen doesn't have much power these days." + Environment.NewLine
		+ "These : I bought these flowers for you." + Environment.NewLine
		+ "Use : Be sure to use a dictionary!" + Environment.NewLine
		+ "Fuse : The fuse had to be replaced." + Environment.NewLine
		+ "Knob : Turn the knob!" + Environment.NewLine
		+ "Tube : I purchased a tube of toothpaste." + Environment.NewLine
		+ "Knew : I knew the store before it was reported." + Environment.NewLine
		+ "Know : Do you know what happened?" + Environment.NewLine
		+ "How : How did the buildings explode?" + Environment.NewLine
		+ "Jet : I am flying in a jet." + Environment.NewLine
		+ "Met : I met my friend." + Environment.NewLine
		+ "Bet : I'll bet you completed your studies." + Environment.NewLine
		+ "Let : God said, \"Let there be light, and there was light\"." + Environment.NewLine
		+ "Pet : You can pet my dog." + Environment.NewLine
		+ "Net : Grab the net!" + Environment.NewLine
		+ "Debt : Never go in debt unless you have to." + Environment.NewLine
		+ "Quit : Sam quit his job." + Environment.NewLine
		+ "Quiet : The machine is very quiet." + Environment.NewLine
		+ "Lieu : He decided to be a businessman in lieu of a baseball player." + Environment.NewLine
		+ "Kite : Joe received a kite for his birthday." + Environment.NewLine
		+ "Night : The night is beautiful." + Environment.NewLine
		+ "Light : Jesus is the light of the world." + Environment.NewLine
		+ "Create : God can create anything from nothing." + Environment.NewLine
		+ "Crack : There is a crack in the windshield." + Environment.NewLine
		+ "Sack : The sack is very large." + Environment.NewLine
		+ "Pack : Can you carry the pack?" + Environment.NewLine
		+ "Sound : The sound of the drill is hurting my ears." + Environment.NewLine
		+ "Told : I was told that the rumor was false." + Environment.NewLine
		+ "Sold : I sold five hundred pencils." + Environment.NewLine
		+ "Bold : Don't be bold to your parents." + Environment.NewLine
		+ "Cold : The weather is very cold." + Environment.NewLine
		+ "Mold : The walls are full of mold." + Environment.NewLine
		+ "lord : He is the lord of the household." + Environment.NewLine
		+ "Hold : Hold on tight!" + Environment.NewLine
		+ "Gold : Gold is a very precious metal." + Environment.NewLine
		+ "Fold : Fold the paper along the creases." + Environment.NewLine
		+ "Word : Can he spell that word?" + Environment.NewLine
		+ "Metal : Most things these days don't have much metal." + Environment.NewLine
		+ "Thing : Throw that old thing away." + Environment.NewLine
		+ "Queue : There are a few documents in the queue." + Environment.NewLine
		+ "Sick : The boy is sick." + Environment.NewLine
		+ "Lick : Lick the envelope to seal it." + Environment.NewLine
		+ "Rick : Rick is a nice boy." + Environment.NewLine
		+ "Pick : We are going to pick apples." + Environment.NewLine
		+ "Dick : Dick is a great friend." + Environment.NewLine
		+ "Tick : A tick is a parasitic insect." + Environment.NewLine
		+ "Mike : I love Mike." + Environment.NewLine
		+ "Thick : The rope is very thick." + Environment.NewLine
		+ "Nick : Nick and Sam are great friends." + Environment.NewLine
		+ "Mick : Mick loves playing with me." + Environment.NewLine
		+ "Sam : Sam is an obedient boy." + Environment.NewLine
		+ "Man : The man was friendly." + Environment.NewLine
		+ "Woman : The woman at the coffee shop treated us very well." + Environment.NewLine
		+ "Men : There have been great men." + Environment.NewLine
		+ "Women : I know many women." + Environment.NewLine
		+ "Boy : That boy is very smart." + Environment.NewLine
		+ "Girl : That girl is very wise." + Environment.NewLine
		+ "Wise : We should all try to be wise." + Environment.NewLine
		+ "Cue : We have an additional cue in solving the problem.";
		public static string IntermediateWords =
		"Advance : That was a great advance for him." + Environment.NewLine
		+ "Application : I have a very useful application." + Environment.NewLine
		+ "Computer : I use my computer to perform many tasks." + Environment.NewLine
		+ "Delinquent : I received a delinquent notice." + Environment.NewLine
		+ "Stage : My friend appeared on the stage." + Environment.NewLine
		+ "Notification : I received a notification that my books were soon due." + Environment.NewLine
		+ "Reminder : I received a friendly reminder." + Environment.NewLine
		+ "Courageous : The man is very courageous." + Environment.NewLine
		+ "Friendly : My co-workers are very friendly." + Environment.NewLine
		+ "Copyright : I own a very inclusive copyright." + Environment.NewLine
		+ "Patent : I received a patent on my invention." + Environment.NewLine
		+ "Allowance : I receive a weekly allowance." + Environment.NewLine
		+ "Monthly : I attend the monthly church meeting." + Environment.NewLine
		+ "Birthday : I look forward to my birthday." + Environment.NewLine
		+ "Queen : A queen doesn't have much power these days." + Environment.NewLine
		+ "Hound : That hound is a very big dog!" + Environment.NewLine
		+ "Mound : Can you make a huge mound?" + Environment.NewLine
		+ "Sweat : When one exercises, they can begin to sweat." + Environment.NewLine
		+ "Sugar : Sugar is very sweet." + Environment.NewLine
		+ "Sweet : The chocolate is very sweet." + Environment.NewLine
		+ "Crude : They used crude pieces of wood to make " + Environment.NewLine
		+ "that dresser." + Environment.NewLine
		+ "Ant : We bought ant traps." + Environment.NewLine
		+ "Gnat : I have been bitten by a gnat." + Environment.NewLine
		+ "Peppy : My younger brother is very peppy." + Environment.NewLine
		+ "Naked : Adam and Eve were naked before God supplied them with clothing." + Environment.NewLine
		+ "Fail : Do not fail your test again!" + Environment.NewLine
		+ "Male : I saw the male bear." + Environment.NewLine
		+ "Mail : Susan, give me the mail." + Environment.NewLine
		+ "Locket : Give me the locket." + Environment.NewLine
		+ "Socket : The socket is out of place." + Environment.NewLine
		+ "Rocket : Look at the rocket!" + Environment.NewLine
		+ "Pocket : I reached in my pocket." + Environment.NewLine
		+ "Insane : Do not act like you're insane." + Environment.NewLine
		+ "Quit : Sam quit his job." + Environment.NewLine
		+ "Quiet : The machine is very quiet." + Environment.NewLine
		+ "Queue : There are a few documents in the queue." + Environment.NewLine
		+ "Lieu : He decided to be a businessman in lieu of a " + Environment.NewLine
		+ "baseball player." + Environment.NewLine
		+ "Debt : Never go in debt unless you have to." + Environment.NewLine
		+ "Meat : Be sure to cook the meat well." + Environment.NewLine
		+ "Meet : I'm going to meet the president." + Environment.NewLine
		+ "Pane : The window pane is dirty." + Environment.NewLine
		+ "Pain : I have a pain in my side." + Environment.NewLine
		+ "Tight : My pants are too tight." + Environment.NewLine
		+ "Persevere : Persevere in all things." + Environment.NewLine
		+ "Crystal : My dishes are crystal clear." + Environment.NewLine
		+ "Sparkle : Look at the sparkle!" + Environment.NewLine
		+ "Vehicle : I just purchased a new vehicle." + Environment.NewLine
		+ "Toilet : Our toilet had to be replaced." + Environment.NewLine
		+ "Resume : I'm about to resume the movie." + Environment.NewLine
		+ "Résumé : I sent my Résumé to many businesses." + Environment.NewLine
		+ "Occur : Many issues can occur after upgrading a computer." + Environment.NewLine
		+ "Misnomer : Zip-Lock bags is a misnomer." + Environment.NewLine
		+ "Mistakenly : We mistakenly asked for Jim, instead of John." + Environment.NewLine
		+ "Accidentally : We accidentally hit that person's car." + Environment.NewLine
		+ "Accident : We got into a major accident." + Environment.NewLine
		+ "Repair : Our car needed a major repair." + Environment.NewLine
		+ "Appliance : Just like computers, a dishwasher is an appliance that can make life easier." + Environment.NewLine
		+ "Create : God can create anything from nothing." + Environment.NewLine
		+ "Beginning : Beginning now, gas prices are skyrocketing." + Environment.NewLine
		+ "Skyrocket : Business owners should try not to skyrocket their prices." + Environment.NewLine
		+ "Curse : We must never curse." + Environment.NewLine
		+ "Crazy : The crazy idea disappeared." + Environment.NewLine
		+ "Disappear : Don't disappear when I call you!" + Environment.NewLine
		+ "Completely : The house is completely flat after the fire." + Environment.NewLine
		+ "Ambition : Napoleon Bonaparte was a man of great ambition." + Environment.NewLine
		+ "Curiosity : We must not have too much curiosity." + Environment.NewLine
		+ "Curious : Little children are very curious." + Environment.NewLine
		+ "Ostentatious : Look at that ostentatious vehicle." + Environment.NewLine
		+ "Pert : We must not elect that pert leader." + Environment.NewLine
		+ "Perk : Breakfast is one of the perks of this job." + Environment.NewLine
		+ "Smile : The baby gave a beautiful smile." + Environment.NewLine
		+ "Locomotive : The locomotive is going by." + Environment.NewLine
		+ "Cruise : Don't cruise too fast." + Environment.NewLine
		+ "Stately : My friend is very stately." + Environment.NewLine
		+ "Impossible : With God, nothing is impossible." + Environment.NewLine
		+ "Uncanny : The uncanny apparition was not believable." + Environment.NewLine
		+ "Situation : We should try not to put ourselves in an awkward situation." + Environment.NewLine
		+ "Awkward : That box is very awkward." + Environment.NewLine
		+ "Situate : Situate the box near the fireplace." + Environment.NewLine
		+ "Appearance : That car has a nice appearance." + Environment.NewLine
		+ "Odor : What's that unpleasant odor?" + Environment.NewLine
		+ "Aroma : Do you smell the sweet aroma of the flowers?" + Environment.NewLine
		+ "Heart : He has a very loving heart." + Environment.NewLine
		+ "Empathetic : He is very empathetic." + Environment.NewLine
		+ "Empathetically : He empathetically showed his love for her." + Environment.NewLine
		+ "Pathetic : The state of that dresser is pathetic." + Environment.NewLine
		+ "Sensible : A sensible lady will ensure that her fiancé is right for her before marrying him." + Environment.NewLine
		+ "Patient : Those children are very patient. " + Environment.NewLine
		+ "Scour : Scour the pans." + Environment.NewLine
		+ "Negligent : Never be negligent of your chores." + Environment.NewLine
		+ "Automotive : He owns the local automotive." + Environment.NewLine
		+ "Instantiate : Did you instantiate how to multiply and divide fractions." + Environment.NewLine
		+ "Enumerate : Please enumerate the list for me." + Environment.NewLine
		+ "Integer : What is the nearest integer of 3.75?" + Environment.NewLine
		+ "Integral : It is integral that children learn how to read and write." + Environment.NewLine
		+ "Ameliorate : Taking Vitamin C can help ameliorate your health." + Environment.NewLine
		+ "Improve : The president is trying to improve the living conditions." + Environment.NewLine
		+ "Scourge : We must never scourge anyone." + Environment.NewLine
		+ "Mutate : Insects mutate very slowly." + Environment.NewLine
		+ "Sixth : Has the sixth visit gone well?" + Environment.NewLine
		+ "Fifteenth : What day is the fifteenth?" + Environment.NewLine
		+ "Fourteenth : The fourteenth is Easter." + Environment.NewLine
		+ "Thirteenth : How is your thirteenth paper going?" + Environment.NewLine
		+ "Twelfth : How is your twelfth essay?" + Environment.NewLine
		+ "First : My first paper is of uttermost importance." + Environment.NewLine
		+ "Tenth : My tenth visit went well." + Environment.NewLine
		+ "Ninth : Have you written your ninth essay yet?" + Environment.NewLine
		+ "Sixteenth : The sixteenth is a Friday." + Environment.NewLine
		+ "Seventeenth : The seventeenth is just eight days from Christmas." + Environment.NewLine
		+ "Eighteenth : The eighteenth is my friend's birthday." + Environment.NewLine
		+ "Nineteenth : The nineteenth day is tomorrow." + Environment.NewLine
		+ "Twentieth : The twentieth week celebrates the beginning of that organization." + Environment.NewLine
		+ "Seventh : My seventh visit has been a success!" + Environment.NewLine
		+ "Second : My second paper received an excellent grade!" + Environment.NewLine
		+ "Third : I love my third dog!" + Environment.NewLine
		+ "Fourth : My fourth dog is wonderful!" + Environment.NewLine
		+ "Fifth : This is the fifth day!" + Environment.NewLine
		+ "Sixth : The sixth day is tomorrow." + Environment.NewLine
		+ "Eighth : The eighth week is next month." + Environment.NewLine
		+ "Worsen : Taking too much medication can worsen one's health." + Environment.NewLine
		+ "Carve : Carefully carve the figure." + Environment.NewLine
		+ "Crank : Crank the engine one more time." + Environment.NewLine
		+ "Prank : Stop performing that prank!" + Environment.NewLine
		+ "Presence : We need more presence." + Environment.NewLine
		+ "Color : Is blue a primary color?" + Environment.NewLine
		+ "Battery : The vehicle needed a new battery." + Environment.NewLine
		+ "Great : What a great day!" + Environment.NewLine
		+ "Substantial : We have some substantial meals." + Environment.NewLine
		+ "Solid : It is important to have a solid education." + Environment.NewLine
		+ "Fixture : We might have to install a new fixture." + Environment.NewLine
		+ "Install : To install a new oven, you must first disconnect the power." + Environment.NewLine
		+ "Coordinate : We need to coordinate all of our members." + Environment.NewLine
		+ "Respond : Did you respond to the call?" + Environment.NewLine
		+ "Broad : We have a broad collection of books." + Environment.NewLine
		+ "Library : We went to the library yesterday." + Environment.NewLine
		+ "Librarian : The Librarian helped us." + Environment.NewLine
		+ "Ordinary : It wasn't an ordinary visit." + Environment.NewLine
		+ "Camouflage : Those tents camouflage with the trees." + Environment.NewLine
		+ "Reptile : A snake is a reptile." + Environment.NewLine
		+ "Amphibian : A frog is an amphibian." + Environment.NewLine
		+ "Spectacular : The student is spectacular." + Environment.NewLine
		+ "Lovely : Look as the lovely view." + Environment.NewLine
		+ "Mountain : The mountain is beautiful." + Environment.NewLine
		+ "Beautiful : Look at the beautiful sunset!" + Environment.NewLine
		+ "View : What a lovely view!" + Environment.NewLine
		+ "Sunset : What a gorgeous sunset!" + Environment.NewLine
		+ "Gorgeous : Look at that gorgeous sedan!" + Environment.NewLine
		+ "Imperial : The imperial mansion is eight miles away." + Environment.NewLine
		+ "Mansion : Look at the mansion." + Environment.NewLine
		+ "Creator : God is the Creator of all things." + Environment.NewLine
		+ "Sing : Sing slowly and passionately." + Environment.NewLine
		+ "Complete : Complete your homework diligently." + Environment.NewLine
		+ "Infinite : His career opened infinite job opportunities." + Environment.NewLine
		+ "Opportunity : What a great opportunity!" + Environment.NewLine
		+ "Systematic : The new program featured a systematic approach." + Environment.NewLine
		+ "Integrate : Be sure to integrate math into the program. " + Environment.NewLine
		+ "Sound : The sound of the drill is hurting my ears." + Environment.NewLine
		+ "Paralyze : Lyme Disease can paralyze you." + Environment.NewLine
		+ "Paralytic : Jesus cured the paralytic." + Environment.NewLine
		+ "Cavalry : The cavalry marched to the scene." + Environment.NewLine
		+ "Calvary : Jesus died at Calvary." + Environment.NewLine
		+ "Contrition : We must have contrition for our sins." + Environment.NewLine
		+ "These : Do you want these apples?" + Environment.NewLine
		+ "Thing : Throw that old thing away." + Environment.NewLine
		+ "Woman : The woman at the coffee shop treated us very well." + Environment.NewLine
		+ "Women : I know many women." + Environment.NewLine
		+ "Thick : The rope is very thick." + Environment.NewLine
		+ "Temperature : The temperature is just 28˚ Fahrenheit.";
		public static string AdvancedWords =
		"Advance : That was a great advance for him." + Environment.NewLine
		+ "Application : I have a very useful application." + Environment.NewLine
		+ "Computer : I use my computer to perform many tasks." + Environment.NewLine
		+ "Delinquent : I received a delinquent notice." + Environment.NewLine
		+ "Notification : I received a notification that my books were soon due." + Environment.NewLine
		+ "Reminder : I received a friendly reminder." + Environment.NewLine
		+ "Courageous : The man is very courageous." + Environment.NewLine
		+ "Friendly : My co-workers are very friendly." + Environment.NewLine
		+ "Copyright : I own a very inclusive copyright." + Environment.NewLine
		+ "Allowance : I received a weekly allowance." + Environment.NewLine
		+ "Monthly : I attend the monthly church meeting." + Environment.NewLine
		+ "Birthday : I look forward to my birthday." + Environment.NewLine
		+ "Queen : A queen doesn't have much power these days." + Environment.NewLine
		+ "Gnat : I have been bitten by a gnat." + Environment.NewLine
		+ "Naked : Adam and Eve were naked before God supplied them with clothing." + Environment.NewLine
		+ "Insane : Do not act like you're insane." + Environment.NewLine
		+ "Quiet : The machine is very quiet." + Environment.NewLine
		+ "Queue : There are a few documents in the queue." + Environment.NewLine
		+ "Lieu : He decided to be a businessman in lieu of a " + Environment.NewLine
		+ "baseball player." + Environment.NewLine
		+ "Debt : Never go in debt unless you have to." + Environment.NewLine
		+ "Persevere : Persevere in all things." + Environment.NewLine
		+ "Crystal : My dishes are crystal clear." + Environment.NewLine
		+ "Sparkle : Look at the sparkle!" + Environment.NewLine
		+ "Vehicle : I just purchased a new vehicle." + Environment.NewLine
		+ "Toilet : Our toilet had to be replaced." + Environment.NewLine
		+ "Resume : I'm about to resume the movie." + Environment.NewLine
		+ "Résumé : I sent my Résumé to many businesses." + Environment.NewLine
		+ "Occur : Many issues can occur after upgrading a computer." + Environment.NewLine
		+ "Misnomer : Zip-Lock bags is a misnomer." + Environment.NewLine
		+ "Mistakenly : We mistakenly asked for Jim, instead of John." + Environment.NewLine
		+ "Accidentally : We accidentally hit that person's car." + Environment.NewLine
		+ "Accident : We got into a major accident." + Environment.NewLine
		+ "Repair : Our car needed a major repair." + Environment.NewLine
		+ "Appliance : Just like computers, appliances can make life easier." + Environment.NewLine
		+ "Create : God can create anything from nothing." + Environment.NewLine
		+ "Beginning : Beginning now, gas prices are skyrocketing." + Environment.NewLine
		+ "Skyrocket : Business owners should try not to skyrocket their prices." + Environment.NewLine
		+ "Curse : We must never curse." + Environment.NewLine
		+ "Crazy : The crazy idea disappeared." + Environment.NewLine
		+ "Disappear : Don't disappear when I call you!" + Environment.NewLine
		+ "Completely : The house is completely flat after the fire." + Environment.NewLine
		+ "Ambition : Napoleon Bonaparte was a man of great ambition." + Environment.NewLine
		+ "Curiosity : We must not have too much curiosity." + Environment.NewLine
		+ "Curious : Little children are very curious." + Environment.NewLine
		+ "Ostentatious : Look at that ostentatious vehicle." + Environment.NewLine
		+ "Pert : We must not elect that pert leader." + Environment.NewLine
		+ "Perk : One of the perks of this job is free breakfast." + Environment.NewLine
		+ "Smile : The baby gave a beautiful smile." + Environment.NewLine
		+ "Locomotive : The locomotive is going by." + Environment.NewLine
		+ "Cruise : Don't cruise too fast." + Environment.NewLine
		+ "Stately : My friend is very stately." + Environment.NewLine
		+ "Impossible : With God, nothing is impossible." + Environment.NewLine
		+ "Uncanny : The uncanny apparition was not believable." + Environment.NewLine
		+ "Situation : We should try not to put ourselves in an awkward situation." + Environment.NewLine
		+ "Awkward : That box is very awkward." + Environment.NewLine
		+ "Situate : Situate the box near the fireplace." + Environment.NewLine
		+ "Appearance : That car has a nice appearance." + Environment.NewLine
		+ "Odor : What's that unpleasant odor?" + Environment.NewLine
		+ "Aroma : Do you smell the sweet aroma of the flowers?" + Environment.NewLine
		+ "Heart : He has a very loving heart." + Environment.NewLine
		+ "Empathetic : He is very empathetic." + Environment.NewLine
		+ "Empathetically : He empathetically showed his love for her." + Environment.NewLine
		+ "Pathetic : The state of that dresser is pathetic." + Environment.NewLine
		+ "Sensible : A sensible lady will ensure that her fiancé is right for her before marrying him." + Environment.NewLine
		+ "Patient : Those children are very patient. " + Environment.NewLine
		+ "Scour : Scour the pans." + Environment.NewLine
		+ "Negligent : Never be negligent of your chores." + Environment.NewLine
		+ "Automotive : He owns the local automotive." + Environment.NewLine
		+ "Instantiate : Did you instantiate how to multiply and divide fractions." + Environment.NewLine
		+ "Enumerate : Please enumerate the list for me." + Environment.NewLine
		+ "Integer : What is the nearest integer of 3.75?" + Environment.NewLine
		+ "Integral : It is integral that children learn how to read and write." + Environment.NewLine
		+ "Ameliorate : Taking Vitamin C can help ameliorate your health." + Environment.NewLine
		+ "Improve : The president is trying to improve the living conditions." + Environment.NewLine
		+ "Scourge : We must never scourge anyone." + Environment.NewLine
		+ "Mutate : Insects mutate very slowly." + Environment.NewLine
		+ "Sixth : Has the sixth visit gone well?" + Environment.NewLine
		+ "Fifteenth : What day is the fifteenth?" + Environment.NewLine
		+ "Fourteenth : The fourteenth is Easter." + Environment.NewLine
		+ "Thirteenth : How is your thirteenth paper going?" + Environment.NewLine
		+ "Twelfth : How is your twelfth essay?" + Environment.NewLine
		+ "First : My first paper is of uttermost importance." + Environment.NewLine
		+ "Tenth : My tenth visit went well." + Environment.NewLine
		+ "Ninth : Have you written your ninth essay yet?" + Environment.NewLine
		+ "Sixteenth : The sixteenth is a Friday." + Environment.NewLine
		+ "Seventeenth : The seventeenth is just eight days from Christmas." + Environment.NewLine
		+ "Eighteenth : The eighteenth is my friend's birthday." + Environment.NewLine
		+ "Nineteenth : The nineteenth day is tomorrow." + Environment.NewLine
		+ "Twentieth : The twentieth week celebrates the beginning of that organization." + Environment.NewLine
		+ "Seventh : My seventh visit has been a success!" + Environment.NewLine
		+ "Second : My second paper received an excellent grade!" + Environment.NewLine
		+ "Third : I love my third dog!" + Environment.NewLine
		+ "Fourth : My fourth dog is wonderful!" + Environment.NewLine
		+ "Fifth : This is the fifth day!" + Environment.NewLine
		+ "Sixth : The sixth day is tomorrow." + Environment.NewLine
		+ "Eighth : The eighth week is next month." + Environment.NewLine
		+ "Worsen : Taking too much medication can worsen one's health." + Environment.NewLine
		+ "Carve : Carefully carve the figure." + Environment.NewLine
		+ "Crank : Crank the engine one more time." + Environment.NewLine
		+ "Prank : Stop performing that prank!" + Environment.NewLine
		+ "Presence : We need more presence." + Environment.NewLine
		+ "Color : Is blue a primary color?" + Environment.NewLine
		+ "Battery : The vehicle needed a new battery." + Environment.NewLine
		+ "Substantial : We have some substantial meals." + Environment.NewLine
		+ "Fixture : We might have to install a new fixture." + Environment.NewLine
		+ "Coordinate : We need to coordinate all of our members." + Environment.NewLine
		+ "Respond : Did you respond to the call?" + Environment.NewLine
		+ "Ordinary : It wasn't an ordinary visit." + Environment.NewLine
		+ "Camouflage : The soldiers camouflaged until the enemy disappeared." + Environment.NewLine
		+ "Reptile : A snake is a reptile." + Environment.NewLine
		+ "Amphibian : A frog is an amphibian." + Environment.NewLine
		+ "Spectacular : The student is spectacular." + Environment.NewLine
		+ "Mountain : The mountain is beautiful." + Environment.NewLine
		+ "Beautiful : Look at the beautiful sunset!" + Environment.NewLine
		+ "Lovely : The hills are lovely." + Environment.NewLine
		+ "Sunset : What a gorgeous sunset!" + Environment.NewLine
		+ "Gorgeous : Look at that gorgeous sedan!" + Environment.NewLine
		+ "Imperial : The imperial mansion is eight miles away." + Environment.NewLine
		+ "Mansion : Look at the mansion." + Environment.NewLine
		+ "Creator : God is the Creator of all things." + Environment.NewLine
		+ "Complete : Complete your homework diligently." + Environment.NewLine
		+ "Infinite : His career opened infinite job opportunities." + Environment.NewLine
		+ "Opportunity : What a great opportunity!" + Environment.NewLine
		+ "Systematic : The new program featured a systematic approach." + Environment.NewLine
		+ "Integrate : Be sure to integrate math into the program. " + Environment.NewLine
		+ "Abomination : That abomination is despicable by all men." + Environment.NewLine
		+ "Gallantly : The soldiers gallantly ran toward the tanks" + Environment.NewLine
		+ "Ferocious : The lion was very ferocious." + Environment.NewLine
		+ "Immaculate : The car was in immaculate condition." + Environment.NewLine
		+ "Paradoxical : That paradoxical belief is still held by most people." + Environment.NewLine
		+ "Mandatory : Did you attend the mandatory meeting?" + Environment.NewLine
		+ "Spectator : The spectator helped us." + Environment.NewLine
		+ "Quintuple : Do you know the definition of quintuple." + Environment.NewLine
		+ "Transubstantiation : In the Holy Eucharist, the bread and wine become the Body and Blood of Jesus, which is called transubstantiation." + Environment.NewLine
		+ "Immediate : The patients need immediate help." + Environment.NewLine
		+ "Patient : The patient needs to talk to the doctor." + Environment.NewLine
		+ "Self-explanatory : The meaning of gift-giving on Christmas is self-explanatory." + Environment.NewLine
		+ "Visualize : Can you visualize a pink sky with blue grass?" + Environment.NewLine
		+ "Imagination : Isn't imagination fun?" + Environment.NewLine
		+ "Satire : Be sure to wear appropriate satire for the party." + Environment.NewLine
		+ "Communicate : Communicate clearly in order that others may easily understand what you say." + Environment.NewLine
		+ "Exemplify : Parents should exemplify how to behave." + Environment.NewLine
		+ "Concurrent : The economy is concurrent with the stock market" + Environment.NewLine
		+ "Coincidence : Is it related, or is it a coincidence?" + Environment.NewLine
		+ "Alienate : Be sure to never alienate someone." + Environment.NewLine
		+ "Obstacle : There was a major obstacle." + Environment.NewLine
		+ "Graphite : Did you see the graphite miners?" + Environment.NewLine
		+ "Adorable : Look at the adorable little cottage!" + Environment.NewLine
		+ "Cottage : How much is that cottage?" + Environment.NewLine
		+ "Sympathy : Do you have Sympathy for them?" + Environment.NewLine
		+ "Exaggerate : Don't exaggerate." + Environment.NewLine
		+ "Overstate : We must not overstate the problem." + Environment.NewLine
		+ "Proximity : What is the proximity between those buildings?" + Environment.NewLine
		+ "Calculus : Have you taken a Calculus course?" + Environment.NewLine
		+ "Algebra : Algebra involves solving variables." + Environment.NewLine
		+ "Geometry : In geometry, we learn about plane coordinates." + Environment.NewLine
		+ "Surplus : We should give our surplus to the poor." + Environment.NewLine
		+ "Mathematical : We should mathematically solve the problem." + Environment.NewLine
		+ "Aspire : We should aspire in our spiritual life." + Environment.NewLine
		+ "Everyday : What is your everyday job?" + Environment.NewLine
		+ "Semantic : We learned about the semantic elements." + Environment.NewLine
		+ "Parallel : The two roads are parallel." + Environment.NewLine
		+ "Symmetrical : The shapes are symmetrical." + Environment.NewLine
		+ "Perpendicular : The lines are perpendicular to each other." + Environment.NewLine
		+ "Algebraically : We algebraically solved the problem." + Environment.NewLine
		+ "Equation : Can you solve this equation?" + Environment.NewLine
		+ "Variable : I have variable job hours." + Environment.NewLine
		+ "Constant : The amount of energy remains constant." + Environment.NewLine
		+ "Christmas : At Christmas, Our Divine Savior was born." + Environment.NewLine
		+ "Asymmetrical : The triangles are asymmetrical." + Environment.NewLine
		+ "Profoundly : The man profoundly prayed." + Environment.NewLine
		+ "Sacred : We must treat all sacred things with reverence." + Environment.NewLine
		+ "Saint : Saint John Paul II was a very holy man." + Environment.NewLine
		+ "Reverent : We must be reverent in church. " + Environment.NewLine
		+ "Heaven : Only those who are holy can be in Heaven." + Environment.NewLine
		+ "Sanctify : The Holy Spirit sanctifies us." + Environment.NewLine
		+ "Frantically : The crowd frantically rushed from the house." + Environment.NewLine
		+ "Ancient : In ancient times, things were different than now." + Environment.NewLine
		+ "Appreciate : We must be sure to appreciate all who have helped us." + Environment.NewLine
		+ "Obsolete : Old Technology eventually becomes obsolete." + Environment.NewLine
		+ "Deprecate : W3C may deprecate that element." + Environment.NewLine
		+ "Eventually : The cars eventually went away." + Environment.NewLine
		+ "Diminish : He may diminish his good name if he continues to act boldly." + Environment.NewLine
		+ "Stun : Don't stun him with the flashlight." + Environment.NewLine
		+ "Robotic : The robotic vehicle moved well." + Environment.NewLine
		+ "Anonymously : The crowd signed their name anonymously." + Environment.NewLine
		+ "Intricate : Look at those intricate flowers." + Environment.NewLine
		+ "Delicate : Those statues are very delicate." + Environment.NewLine
		+ "Paralyze : Lyme Disease can paralyze you." + Environment.NewLine
		+ "Paralytic : Jesus cured the paralytic." + Environment.NewLine
		+ "Cavalry : The cavalry marched to the scene." + Environment.NewLine
		+ "Calvary : Jesus died at Calvary." + Environment.NewLine
		+ "Contrition : We must have contrition for our sins." + Environment.NewLine
		+ "Illness : The illness constantly plagued him." + Environment.NewLine
		+ "Plague : The Bubonic Plague was terrible." + Environment.NewLine
		+ "Asynchronously : The program saves files asynchronously." + Environment.NewLine
		+ "Synchronize : Do you synchronize your account?" + Environment.NewLine
		+ "Metadata : Websites involve much metadata." + Environment.NewLine
		+ "Temperature : The temperature is just 28˚ Fahrenheit.";

		public static string Help = "<!Doctype html>"
	+ "<html lang=\"en\">"
	+ "<head>"
	+ "<style type=\"text/css\">"
	+ "h3{font-family:Cambria,Calibri,Times New Roman, Aerial, Sans Serif;}"
	+ "a{color:black;text-decoration:none;padding:5px;font-weight:bold;}"
	+ "a:hover{background-color:gray;color:white}"
	+ "#NavLink{background-color:lightgray;border:1px solid dimgray;padding: 10px;}"
	+ "</style>"
	+ "<title>Spelling Rules | Spelling Master&trade;</title>"
	+ "</head>"
	+ "<body style = \"font-family:Calibri\">"
	+ "<h1 id = \"Main\" style = \"font-family:Cambria;color:blueviolet\">Auto-Helper</h1>"
	+ "<div id = \"NavLink\">"
	+ "<a href = \"#kw\">Kw Sound</a>"
	+ "<a href = \"#lfs\">L/F/S Sounds</a>"
	+ "<a href = \"#k\">K Sound</a>"
	+ "<a href = \"#j\">J Sound</a>"
	+ "<a href = \"#z\">Z Sound</a>"
	+ "<a href = \"#sh\">Sh Sound</a>"
	+ "<a href = \"#all\">All/till/full Sounds</a>"
	+ "<a href = \"#v\">V Sound</a>"
	+ "<hr style = \"color:gray\"/>"
	+ "<a href = \"#iy\">Iy Sound</a>"
	+ "<a href = \"#ay\">Ay Sound</a>"
	+ "<a href = \"#ee\">Ee Sound</a>"
	+ "<hr style = \"color:gray\"/>"
	+ "<a href = \"#general\">Forming Plurals</a>"
	+ "</div>"
	+ "<h2>Spelling Words</h2>";
		public static string Rules = "<h2>Consonant Rules</h2>"
	+ "<p id =\"kw\">The <strong>/kw/</strong> sound is spelled with the two letters <strong>q</strong> and <strong>u</strong>, as in <strong>qu</strong>iet. The letter <strong>q</strong> is always followed by the letter <strong>u</strong>."
	+ "</p>"
	+ "<p id =\"lfs\">The <strong>/l/,</strong> <strong>/f/,</strong> and <strong>/s/</strong>sounds after a single vowel in one-syllable words are often spelled <strong>ll</strong>, <strong>ff</strong>, and <strong>ss</strong>, as in be<strong>ll</strong>, stu<strong>ff</strong>, and Ma<strong>ss</strong>."
	+ "</p>"
	+ ""
	+ "<p id =\"k\">The <strong>/k/</strong> sound after a short vowel is spelled <strong>/ck/</strong>, as in qua<strong>ck</strong>, ne<strong>ck</strong>, qui<strong>ck</strong>, clo<strong>ck</strong>, and du<strong>ck</strong>."
	+ "</p>"
	+ ""
	+ "<p id =\"j\">The <strong>/j/</strong> sound after a short vowel is spelled <strong>/dge/</strong>, as in ba<strong>dge</strong>, ple<strong>dge</strong>, bri<strong>dge</strong>, do<strong>dge</strong>, and fu<strong>dge</strong>."
	+ "</p>"
	+ ""
	+ "<p id =\"z\">The <strong>/z/</strong> sound in <strong>z</strong>oo at the beginning of a root is usually spelled <strong>z</strong>, and never spelled <strong>s</strong>."
	+ "</p>"
	+ ""
	+ "<p id = \"sh\"> The <strong>/sh/</strong> sound at the beginning of a word or end of a syllable is usually spelled <strong>sh</strong>. At the beginning of a syllable other than the first one, it is usually spelled <strong>ti</strong>, <strong>si</strong>, or <strong>ci</strong>. This applies unless the word ends in <strong>-ship</strong>, of course. When the syllable before the <strong>sh</strong> sound ends in <strong>s</strong>, as in mis<strong>si</strong>on, it is spelled <strong>si</strong>."
	+ ""
	+ "<p id = \"all\"><strong>All</strong>, <strong>till </strong>, and <strong>full</strong> are usually spelled with one <strong>l</strong> when they are added to another syllable, as in <strong>al</strong>most, un<strong>til</strong>, and care<strong>ful</strong>."
	+ ""
	+ "<p id = \"v\">The <strong>/v/</strong> at the end of a word is never spelled <strong>v</strong>. It is usually spelled <strong>ve</strong>, as in ha<strong>ve</strong>."
	+ "<h2>Vowel Rules</h2>"
	+ "<p id = \"iy\"> The <strong>/iy/</strong> sound is not spelled <strong>i</strong> at the end of most words.</p>"
	+ "<p id = \"ay\"> The <strong>/iy/</strong> sound is not spelled <strong>a</strong> at the end of most words.</p>"
	+ "<p id = \"ee\"> The <strong>/ee/</strong> sound after c is spelled <strong>ei</strong>, as in rec<strong>ei</strong>ve.</p>"
	+ "<h2>Rules for Forming Plurals</h2>"
	+ "<p id = \"general\">"
	+ "Rules are formed by adding <strong>s</strong> or <strong>es</strong> to the singular noun."
	+ "</p>"
	+ "<p> Simply add <strong>s</strong> to most nouns. </p>"
	+ "<p> Add <strong>es</strong> to nouns ending with <strong>ss</strong>, <strong>x</strong>, <strong>z</strong>, <strong>ch</strong>, or <strong>sh</strong>."
	+ "</p>"
	+ "<p> Change <strong>y</strong> to <strong>i</strong> before adding <strong>es</strong> when a noun ends with <strong>y</strong> preceded by a consonant. When a noun ends with <strong>y</strong> preceded by a vowel, simply add <strong>s</strong>."
	+ "<p>Usually, when a noun ends with <strong>f</strong> or <strong>fe</strong>, simply add <strong>s</strong>. Sometimes, change <strong>f</strong> or <strong>fe</strong> to <strong>v</strong> before adding <strong>es</strong>."
	+ "<p> Add <strong>es</strong> to a noun ending with <strong>o</strong> preceded by a consonant, except some words, such as piano<strong>s</strong>. When a noun ends with <strong>o</strong> preceded by a vowel, simply add <strong>s</strong>."
	+ "<a style = \"bottom:15px;right:10px;position:fixed;font-family:wingdings\" href = \"#Main\">é</a>";
	}

}