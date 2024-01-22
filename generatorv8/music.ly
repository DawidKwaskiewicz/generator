\version "2.22.0"

#(set-default-paper-size "a3")

drumsUpA = { \drummode { 
hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | 
hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | 
hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | 
hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 | hh8 hh8 hh8 hh8 hh8 hh8 hh8 hh8 |\bar "||"}}
drumsDownA = { \drummode { 
bd8. bd16 sn8. bd16 r8. bd16 sn8 bd8 | bd4 sn16 bd8. r8. bd16 sn8 sn8 | r8 bd8 sn4 bd4 sn8. bd16 | r4 sn8. bd16 r8 bd8 sn8. bd16 | 
bd8. bd16 sn8. bd16 r8. bd16 sn8 bd8 | bd4 sn16 bd8. r8. bd16 sn8 sn8 | r8 bd8 sn4 bd4 sn8. bd16 | r4 sn8. bd16 r8 bd8 sn8. bd16 | 
bd8. bd16 sn8. bd16 r8. bd16 sn8 bd8 | bd4 sn16 bd8. r8. bd16 sn8 sn8 | r8 bd8 sn4 bd4 sn8. bd16 | r4 sn8. bd16 r8 bd8 sn8. bd16 | 
bd8. bd16 sn8. bd16 r8. bd16 sn8 bd8 | bd4 sn16 bd8. r8. bd16 sn8 sn8 | r8 bd8 sn4 bd4 sn8. bd16 | r4 sn8. bd16 r8 bd8 sn8. bd16 |\bar "||"}}
bassGuitarA = {
bes,,8. 16 8. 16~ des,8. 16 8 8 | des,4 16 8.~ ees,8. 16 8 8~ | bes,,8 8 4 des,4 8. 16~ | des,4 8. 16~ ees,8 8 8. 16 |
bes,,8. 16 8. 16~ des,8. 16 8 8 | des,4 16 8.~ ees,8. 16 8 8~ | bes,,8 8 4 des,4 8. 16~ | des,4 8. 16~ ees,8 8 8. 16 |
bes,,8. 16 8. 16~ des,8. 16 8 8 | des,4 16 8.~ ees,8. 16 8 8~ | bes,,8 8 4 des,4 8. 16~ | des,4 8. 16~ ees,8 8 8. 16 |
bes,,8. 16 8. 16~ des,8. 16 8 8 | des,4 16 8.~ ees,8. 16 8 8~ | bes,,8 8 4 des,4 8. 16~ | des,4 8. 16~ ees,8 8 8. 16 |
\bar "||"}
rhythmGuitarA = {
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
<bes, f bes des' f' bes'>16 16 16 16~ 16 16 8 <des' aes' des'' f''>16 8 16~ 16 16 16 16 |<des aes des' f' aes' des''>8. 16~ 16 8 16 <ees bes ees' g'>16 8. 16 16 16 16 |
\bar "||"}
leadVocalsAAA = {
des''4 bes'4 aes'4 f'4 |
aes'4 f'4 des'8 ees'8 r4 |
}
leadVocalsAAB = {
des''4 bes'4 g'4 bes'4 |
aes'4 f'4 ees'8 des'8 r4 |
}
leadVocalsAAC = {
des''4 bes'4 aes'4 r8. f'16 |
ees'4 aes'4 f'8 ees'8 r4 |
}
leadVocalsAAD = {
des''4 bes'4 aes'4 f'8. aes'16 |
aes'8. f'16 des'4~ 16 ees'8. r4 |
}
leadVocalsABA = {
des''4 c''4 bes'4~ 16 c''8. |
aes'4 f'4 bes'8 aes'8 r4 |
}
leadVocalsABB = {
des''4 c''4 bes'4~ 16 c''8. |
aes'4 f'8. aes'16 bes'4 r4 |
}
leadVocalsABC = {
des''4 c''4 bes'4~ 16 g'8.~ |
16 aes'8. f'8. aes'16 bes'8 aes'8 r4 |
}
leadVocalsABD = {
des''4 c''4 bes'4~ 16 c''8. |
aes'4 f'4 bes'8 aes'8 r4 |
}
leadVocalsA = {
\leadVocalsAAA
\leadVocalsABA
\leadVocalsAAB
\leadVocalsABB
\leadVocalsAAC
\leadVocalsABC
\leadVocalsAAD
\leadVocalsABD
}
\score {
\layout { }
<<
\new Staff \with {midiInstrument = "distorted guitar" instrumentName = \markup { \center-column { "Lead" \line { "Vocals" } } } }
{ \clef "G_8" \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \leadVocalsA  }
\new StaffGroup \with { instrumentName = \markup { \center-column { "Rhythm" \line { "Guitar" } } } } <<
\new Staff \with {midiInstrument = "acoustic guitar (steel)" }
{ \clef "G_8" \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \rhythmGuitarA  }
\new TabStaff { \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \rhythmGuitarA  }
>>
\new StaffGroup \with { instrumentName = \markup { \center-column { "Bass" \line { "Guitar" } } } } <<
\new Staff \with {midiInstrument = "electric bass (finger)" }
{ \clef "bass_8" \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \bassGuitarA  }
\new TabStaff \with { stringTunings = #bass-tuning } { \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \bassGuitarA  }
>>
\new DrumStaff \with { instrumentName = "Drums" } <<
  \drummode {
    << { \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \drumsUpA  }
      \\
       { \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \drumsDownA  } >>
  }
>>
>>
}
\score {
\midi { }
<<
\new Staff \with {midiInstrument = "distorted guitar" }
{ \transpose e e' { \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \leadVocalsA } }
\new Staff \with {midiInstrument = "acoustic guitar (steel)" }
{ \transpose e e { \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \rhythmGuitarA } }
\new Staff \with {midiInstrument = "electric bass (finger)" }
{ \transpose e e' { \numericTimeSignature \time 4/4 \tempo 4 = 78 \key aes \major \bassGuitarA } }
\new DrumStaff \with { instrumentName = "Drums" } <<
  \drummode {
    << { \numericTimeSignature \time 4/4 \tempo 4 = 78 \drumsUpA }
      \\
       { \numericTimeSignature \time 4/4 \tempo 4 = 78 \drumsDownA } >>
  }
>>
>>
}
