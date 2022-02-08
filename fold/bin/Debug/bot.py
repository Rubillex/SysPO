#! /usr/bin/env python
# -*- coding: utf-8 -*-
import telebot
import config
import random

import json

from telebot import types


bot = telebot.TeleBot(config.token)

# def hello():
#     helps+=1
#     print(helps)
#
# t = Timer(240.0, hello)

@bot.message_handler(commands=['start'])
def welcome(message):

    sti = open('stiker/sticker.webp', 'rb')
    # bot.send_sticker(message.chat.id, sti)

    helps = 0

        #клавиатура
    # markup = types.ReplyKeyboardMarkup(resize_keyboard=True)
    # item1 = types.KeyboardButton("1")
    # item2 = types.KeyboardButton("2")
    # item3 = types.KeyboardButton("3")
    # item4 = types.KeyboardButton("4")
    # item5 = types.KeyboardButton("5")
    # item6 = types.KeyboardButton("6")
    # item7 = types.KeyboardButton("7")
    #
    #
    # markup.add(item1, item2, item3, item4, item5, item6, item7)
    bot.send_message(message.chat.id, "Вот мы и встретились. Любопытство до добра не доводит, а вот тяга к знаниям – это всегда похвально!")

    bot.send_message(message.chat.id, "Вы пошли по моему пути и оказались здесь. Теперь вам предстоит разгадать то, что не удалось сделать мне - найти все ключи и все загадки, чтобы выбраться. Иначе – вы тоже станете частью этой системы проводов и микросхем. И дверь останется навсегда закрытой для вас.")

    bot.send_message(message.chat.id, "И первый ключ уже у вас в руках. Вам нужно подобрать пароль к компьютеру. Но не все так просто. Существует 50 вариантов – и только один верный, который удовлетворяет 3 критериям парольной политики. Эти правила на поверхности, смотри внимательно. Как только получите доступ к компьютеру – отправьте мне верную комбинацию символов.")



    # bot.send_message(message.chat.id, "Добро пожаловать, {0.first_name}!\n Я - <b>{1.first_name}</b>, бот созданный для Дарины".format(message.from_user, bot.get_me()),
    # parse_mode='html', reply_markup=markup)

@bot.message_handler(content_types=['text'])
def lalal(message):
    if message.chat.type == 'private':

        msg = message.text.upper()

        print(msg)
        print(message.text)

        if message.text == 'KiberNET22':
            bot.send_message(message.chat.id, "Вы видите на экране множество папок. В одной из них следующий ключ – это имя известнейшего программиста современности в сфере информационной безопасности. Оглянитесь, найдите его, и он скажете кодовую фразу. Жду и от вас чистосердечное признание в ответ.")


        # elif message.text == 'Как дела?':
        #
        #     markup = types.InlineKeyboardMarkup(row_width=2)
        #     item1 = types.InlineKeyboardButton("Хорошо", callback_data='good')
        #     item2 = types.InlineKeyboardButton("Не очень", callback_data='bad')
        #
        #     markup.add(item1, item2)
        #
        #     bot.send_message(message.chat.id, 'отлично! А у тебя?', reply_markup=markup)
        #
        #

        elif msg == 'I LOVE YOU':

            bot.send_message(message.chat.id, "Касперский – человек, знающий толк в борьбе с компьютерным злом. И «I Love You» непросто слова. В ночь с 4 на 5 мая 2000 года пользователи всего мира получили это сообщение на свои компьютеры. Впоследствии эта безобидная фраза нанесла огромный ущерб мировой экономике в размере 10-15 миллиардов долларов.")
            bot.send_message(message.chat.id, "О чем идет речь? В какую книгу ЭТО было занесено под фразой о признании в любви? Пользоваться ресурсами всемирной паутины никто не запрещал. \nОтвет на эти два вопроса будет являться 3 ключом на пути к вашему освобождению.")

        elif msg == 'ВИРУС, КНИГА РЕКОРДОВ ГИННЕССА' or msg == 'ВИРУС КНИГА РЕКОРДОВ ГИННЕССА' or msg == 'ВИРУС. КНИГА РЕКОРДОВ ГИННЕССА':


            pic = open('picture.jpg', 'rb')
            bot.send_photo(message.chat.id, pic)

            bot.send_message(message.chat.id, "Книга – кладезь знаний. На ее страницах скрыто сотни тысяч символов, которые несут особый смысл, объединяясь в слова.")
            bot.send_message(message.chat.id, "Ваша задача, составить связный текст (он будет следующей подсказкой) и найти слова, которые составляют название одной известнейшей транснациональной компании по производству программного обеспечения для различного рода вычислительной техники.(В ответе указывайте все слова через запятую)")

        elif msg == 'COMPUTER, SOFTWARE, MICRO' or msg == 'COMPUTER, MICRO, SOFTWARE' or msg == 'MICRO, SOFTWARE, COMPUTER' or msg == 'MICRO, COMPUTER, SOFTWARE' or msg == 'SOFTWARE, MICRO, COMPUTER' or msg == 'SOFTWARE, COMPUTER, MICRO':

            bot.send_message(message.chat.id, "Теперь внимательно прочитайте получившейся текст. Там вас ждёт следующее задание")

        elif msg == 'СОЗДАТЕЛЬ':

            bot.send_message(message.chat.id, "Да. Сейчас речь пойдет о создателе одной известнейшей соц сети – Марке Цукерберге, его портрет вы уже видели в стенах лаборатории. У этого разработчика есть одна особенность, связана она с одним из его органов чувств. Именно благодаря ей Facebook имеет синий интерфейс. Поговорите с ним с глазу на глаз, он расскажет вам, какие два прилагательных являются ключом к свободе.")

        elif msg == 'КРАСНЫЙ ЗЕЛЁНЫЙ' or msg == 'КРАСНЫЙ ЗЕЛЕНЫЙ' or msg == 'КРАСНЫЙ И ЗЕЛЁНЫЙ' or msg == 'КРАСНЫЙ И ЗЕЛЕНЫЙ' or msg == 'КРАСНЫЙ, ЗЕЛЕНЫЙ' or msg == 'КРАСНЫЙ, ЗЕЛЁНЫЙ' or msg == 'ЗЕЛЁНЫЙ КРАСНЫЙ' or msg == 'ЗЕЛЕНЫЙ КРАСНЫЙ' or msg == 'ЗЕЛЁНЫЙ, КРАСНЫЙ' or msg == 'ЗЕЛЕНЫЙ, КРАСНЫЙ' or msg == 'ЗЕЛЁНЫЙ И КРАСНЫЙ' or msg == 'ЗЕЛЕНЫЙ И КРАСНЫЙ':

            voice = open('ZOOM0113.wav', 'rb') #тут голосовое
            bot.send_voice(message.chat.id, voice)

        else:
            bot.send_message(message.chat.id, 'кажется, вы неправы ;)')
            sti = open('stiker/sticker.webp', 'rb')
            # bot.send_sticker(message.chat.id, sti)




@bot.callback_query_handler(func=lambda call: True)
def callback_inline(call):
    try:
        if call.message:
            if call.data == 'good':
                bot.send_message(call.message.chat.id, 'Вот и отлично')
            elif call.data == 'bad':
                bot.send_message(call.message.chat.id, 'Это плохо')

            #Удаляем кнопки
            bot.edit_message_text(chat_id=call.message.chat.id, message_id=call.message.message_id, text="Как дела?",
            reply_markup=None)

            bot.answer_callback_query(callback_query_id=call.id, show_alert=False,
                text="ЭТО ТЕСТОВОЕ УВЕДОМЛЕНИЕ!!11")
    except Exception as e:
        print(repr(e))

bot.polling(none_stop=True)
