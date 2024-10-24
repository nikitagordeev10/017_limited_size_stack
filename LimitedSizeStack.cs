using System;

namespace LimitedSizeStack {

    public class LimitedSizeStack<T> {

        private T[] items; // массив items элементов типа T
        private int top = 0; // позиция вершины
        private int stackSize = 0; // размер стека

        // Ограничение размера стека
        public LimitedSizeStack(int undoLimit) {
            // создаём массив items, размера undoLimit, сосотоящий из элементов типа T
            items = new T[undoLimit];
        }

        // Добавить на вершину стека
        public void Push(T item) {
            // Если ограничение размера стека 0, то ничего не добавляем
            if (items.Length == 0)
                return;
            // Иначе, добавить элемент на вершину стека (DropOutStack)
            items[top] = item;
            top = (top + 1) % items.Length;
            // Если стек заполнен не до конца, то запомнить размер
            if (stackSize < items.Length)
                stackSize++;
        }

        // Удалить с вершины стека
        public T Pop() {
            // Если стек пуст, то выдаём ошибку
            if (stackSize == 0)
                throw new System.InvalidOperationException("Stack is empty");
            // Иначе, удалить элемент с вершины стка (DropOutStack)
            top = ((items.Length - 1) + top) % items.Length;
            stackSize--;
            return items[top];
        }

        // Текущий размер стека
        public int Count {
            get {
                return stackSize;
            }
        }
    }
}

/* Подсказки
 * Для эффективной реализации такого стека подойдет двусвязный список. Он уже реализован в классе LinkedList
 * LinkedList не реализует интерфейс IList, поэтому Last() для него работает медленно. Однако у него есть собственное свойство Last, работающее быстро.
 */

/* Материалы
 * Могу ли я ограничить глубину общего стека? // stackovergo URL: https://stackovergo.com/ru/q/54316/can-i-limit-the-depth-of-a-generic-stack (дата обращения: 19.02.2023).
 * DropOutStack // URL: https://courses.cs.vt.edu/~cs2704/spring04/projects/DropOutStack.pdf (дата обращения: 19.02.2023).
 */