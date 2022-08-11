### File Chunks Hash Viewer

Программа для расчета SHA256 хеша блоков файла

------------

#### Запуск:

`.\FileChunksHashViewer.exe {file} {block_size} {is_strong_order} {process_count}`

**{file}** - Путь к файлу, обязательный параметр (например test.txt)

**{block_size}** - Размер блока для расчета хеша, обязательный параметр (например 256)

**{is_strong_order}** - Выводить ли хеш в строгом порядке от 1 до N (например true)

**{process_count}** - Количество потоков для обработки файла. По умолчанию программа сама подбирает необходимое оптимальное количество. (например 8)

Пример запуска:

`.\FileChunksHashViewer.exe Examples\test.txt 2 true`