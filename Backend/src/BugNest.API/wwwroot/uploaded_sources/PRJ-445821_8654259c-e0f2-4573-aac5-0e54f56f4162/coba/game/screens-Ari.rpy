## ===============================
## SCREEN UNTUK TAMPILKAN STATS
## (di screens.rpy)
## ===============================

screen show_stats_screen():

    tag menu
    modal True

    frame:
        background "#0008"
        padding (40, 30)
        xalign 0.5
        yalign 0.5

        vbox:
            spacing 15
            text "🎲 DICE ROLL: [stat_roll]" size 30 color "#FFD700" xalign 0.5

            grid 2 5 spacing 10:
                for stat in stat_result:
                    text "[stat]" size 24
                    text "[stat_result[stat]]" size 24

            if not has_rerolled:
                textbutton "🎲 Reroll (1x)" action [Function(reroll_stats), Show("show_stats_screen", transition=dissolve)]
            else:
                textbutton "❌ Sudah Reroll" action None

            textbutton "✅ Lanjutkan" action Return()

## Selesai — ini bisa jadi fondasi RPG Stat & Roll kamu. Tambahkan integrasi ke battle, dialog, skill selanjutnya!
