﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Telegram.Bot;

namespace StudentAssistantTelegramBot
{
    public class OnMessage
    {
        public static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Student stud;
            Program.students.ContainsStudByID(e.Message.Chat.Id, out stud);

            string message = "no_message";
            string answer = "no_answer";

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                message = e.Message.Text.ToLower();
            else
                return;

            Console.Write($"{DateTime.Now} | from: {e.Message.From.Username} | text: {message} | level before: {stud.users_loc}");

            /* =================================== РАБОТАЮТ ВЕЗДЕ =================================== */
            if (message == "/start")
            {
                answer = $"Привет, студент! Я бот-помощник в подготовке к сессии, моя задача - помочь тебе " +
                    $"правильно распределить время в подготовке к сессии, составив расписание, и " +
                    $"стимулировать начать готовиться к ней периодическими напоминаниями.\n\n" +
                    $"Сейчас ты в главном меню.\n" +
                    $"Отсюда ты можешь перейти:\n" +
                    $"• в раздел учёбы командой /study\n" +
                    $"• в раздел развлечений командой /fan\n\n" +
                    $"Ну что, будем учиться или отдохнём?";
                stud.users_loc = LevelOfCode.MAIN_MENU;
            }
            else if (message == "/menu")
            {
                answer = $"Ты в главном меню.\n" +
                    $"Отсюда ты можешь перейти:\n" +
                    $"• в раздел учёбы командой /study\n" +
                    $"• в раздел развлечений командой /fan\n\n" +
                    $"Ну что, будем учиться или отдохнём?";
                stud.users_loc = LevelOfCode.MAIN_MENU;
            }
            /* =================================== MAIN_MENU =================================== */
            else if (message == "/fan" && stud.users_loc == LevelOfCode.MAIN_MENU)
            {
                answer = $"Ты в меню развлечений.\n" +
                    $"Здесь я могу:\n" +
                    $"• рассказать тебе случайный анекдот (команда /joke)\n" +
                    $"• порекомендовать музыку (команда /music)\n\n" +
                    $"Вернуться в главное меню - команда /menu";
                stud.users_loc = LevelOfCode.FAN_MENU;
            }
            else if (message == "/study" && stud.users_loc == LevelOfCode.MAIN_MENU)
            {
                answer = $"Ты в меню подготовки к сессии. ✏\n" +
                    $"Здесь я могу:\n" +
                    $"• составить тебе расписание для сессии (команда /makeschedule)\n" +
                    $"• дать совет по подготовке к сессии (команда /advice)\n\n" +
                    $"Вернуться в главное меню - команда /menu";
                stud.users_loc = LevelOfCode.STUDY_MENU;
            }
            /* =================================== FAN_MENU =================================== */
            else if (message == "/joke" && stud.users_loc == LevelOfCode.FAN_MENU)
            {
                answer = "шутка";
            }
            else if (message == "/music" && stud.users_loc == LevelOfCode.FAN_MENU)
            {
                answer = "музыка";
            }
            /* =================================== STUDY_MENU =================================== */
            else if (message == "/makeschedule" && stud.users_loc == LevelOfCode.STUDY_MENU)
            {
                answer = "расписание";
            }
            else if (message == "/advice" && stud.users_loc == LevelOfCode.STUDY_MENU)
            {
                answer = "советов нет, но вы держитесь";
            }
            /* ================================================================================== */

            Console.WriteLine($" | level after: {stud.users_loc}");

            if (answer == "no_answer")
                return;

            Program.Bot.SendTextMessageAsync(stud.student_id, answer); // отправка сообщения
        }
    }
}