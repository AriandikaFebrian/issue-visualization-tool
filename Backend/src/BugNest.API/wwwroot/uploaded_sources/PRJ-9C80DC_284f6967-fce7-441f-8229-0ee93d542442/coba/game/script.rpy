## ===================================
## REN'PY ISEKAI RPG DICE ROLL SYSTEM
## ===================================

# 📌 Tambahkan ke script.rpy

label start:

    scene black with fade

    "Kepalaku terasa berat..."
    "Tiba-tiba pandanganku mengabur."

    play sound "sfx/portal.wav"

    "Tubuhku seperti tertarik oleh sesuatu yang tidak terlihat..."
    "Aku tidak bisa melawan..."

    scene bg isekai_portal with dissolve
    "Cahaya terang menyilaukan mataku..."

    scene bg forest_day with fade
    "..."
    "Angin sepoi-sepoi... aroma rumput... suara dedaunan..."

    "Perlahan aku membuka mata."
    "Aku terbaring... di bawah pohon?"
    "Tapi... ini bukan kamarku."
    "Ini... dunia lain?"

    jump intro_name_input

label intro_name_input:

    "???" "Hei! Kau sadar?"

    "Aku perlahan duduk dan mencoba memahami situasi..."

    $ player_name = renpy.input("Namamu siapa?", length=20)
    $ player_name = player_name.strip()
    if player_name == "":
        $ player_name = "Asta"

    "Namaku... [player_name]..."

    jump roll_stat_intro

label roll_stat_intro:

    "Sebuah cahaya biru muncul di depan mataku..."
    "Sistem misterius muncul di udara..."

    call roll_stat_screen
    return

label roll_stat_screen:
    $ initial_roll_stats()
    call screen show_stats_screen
    return


## ===============================
## PYTHON BLOCK (di script.rpy)
## ===============================

init python:
    import random

    stat_pool = ["STR", "DEX", "INT", "LUK", "VIT", "CHA", "WIS", "AGI", "PER", "RES"]

    def generate_stats(dice_roll):
        base = 3
        bonus = int(dice_roll / 4)
        stats = {}
        shuffled = stat_pool[:]
        random.shuffle(shuffled)
        for stat in shuffled:
            stats[stat] = base + random.randint(0, bonus)
        return stats

    def reroll_stats():
        store.stat_roll = random.randint(1, 20)
        store.stat_result = generate_stats(store.stat_roll)
        store.has_rerolled = True

    def initial_roll_stats():
        store.stat_roll = random.randint(1, 20)
        store.stat_result = generate_stats(store.stat_roll)
        store.has_rerolled = False

