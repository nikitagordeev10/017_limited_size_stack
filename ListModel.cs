using System;
using System.Collections.Generic;

namespace LimitedSizeStack {
    public class ListModel<TItem> {
        public List<TItem> Items { get; }
        public int UndoLimit;
        private LimitedSizeStack<Action> undoActions;

        public ListModel(int undoLimit) {  // Конструктор класса
            Items = new List<TItem>();
            UndoLimit = undoLimit;
            undoActions = new LimitedSizeStack<Action>(undoLimit);
        }

        public ListModel(List<TItem> items, int undoLimit) { // Конструктор класса
            Items = items;
            UndoLimit = undoLimit;
            undoActions = new LimitedSizeStack<Action>(undoLimit);
        }

        public void AddItem(TItem item) { // добавление нового элемента в список
            Items.Add(item); // добавление элемента в список
            AddUndoAction(new Action(RemoveItemFromList));
        }

        public void RemoveItem(int index) { // Удаление элемента из списка по индексу
            var item = Items[index];
            Items.RemoveAt(index);
            Action undoAction = InsertItemAtIndex; // Создание действия для отмены удаления элемента
            void InsertItemAtIndex() { // Локальная функция добавления элемента на прежнее место
                Items.Insert(index, item);
            }
            AddUndoAction(undoAction); // Добавление действия отмены удаления элемента
        }

        public bool CanUndo() { // Проверка на возможность выполнения операции отмены
            return undoActions.Count > 0;
        }

        public void Undo() { // Отмена последнего действия
            if (CanUndo()) { 
                var action = undoActions.Pop(); // Если в списке undoActions есть элемент
                action.Invoke(); // выполняется последнее добавленное действие
            }
        }

        private void AddUndoAction(Action action) { // Добавление действия отмены последнего действия в стек
            undoActions.Push(action);
        }

        private void RemoveItemFromList() { // Удаление последнего элемента из списка
            Items.RemoveAt(Items.Count - 1);
        }
    }
}