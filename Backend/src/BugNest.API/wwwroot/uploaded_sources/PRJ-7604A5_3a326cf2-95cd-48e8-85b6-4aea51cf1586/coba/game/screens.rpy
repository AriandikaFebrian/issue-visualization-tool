screen main_menu():

    tag menu
    add "coba.png"

    frame:
        background None
        xalign 0.05  # Geser frame ke kiri (0 = paling kiri, 0.5 = tengah)
        yalign 0.5   # Tengah vertikal

        vbox:
            spacing 15

            textbutton "Start Game" action Start():
                text_font "MochiyPopOne-Regular.ttf"
                text_size 30
                text_color "#FFFFFF"
                background "#0008"
                hover_background "#FFD70022"
                xminimum 250
                xalign 0.0  # Rata kiri

            textbutton "Load Game" action ShowMenu("load"):
                text_font "MochiyPopOne-Regular.ttf"
                text_size 30
                text_color "#FFFFFF"
                background "#0008"
                hover_background "#FFD70022"
                xminimum 250
                xalign 0.0

            textbutton "Save Game" action ShowMenu("save"):
                text_font "MochiyPopOne-Regular.ttf"
                text_size 30
                text_color "#FFFFFF"
                background "#0008"
                hover_background "#FFD70022"
                xminimum 250
                xalign 0.0

            textbutton "Preferences" action ShowMenu("preferences"):
                text_font "MochiyPopOne-Regular.ttf"
                text_size 30
                text_color "#FFFFFF"
                background "#0008"
                hover_background "#FFD70022"
                xminimum 250
                xalign 0.0

            textbutton "Quit" action Quit(confirm=True):
                text_font "MochiyPopOne-Regular.ttf"
                text_size 30
                text_color "#FFFFFF"
                background "#0008"
                hover_background "#FFD70022"
                xminimum 250
                xalign 0.0
