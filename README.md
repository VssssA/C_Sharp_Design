# C_Sharp_Design

Оптимизатор маршрута

Команда: 
Шмаков Данил Юрьевич
Степанов Вадим Витальевич РИ-310910 (АТ-02)
Меркулов Андрей Владимирович РИ-310945 (АТ-02)
Занков Никита Евгеньевич РИ-310940 (Ат-02)

Описание:
Проект "Оптимизатор маршрута" разработан для поиска оптимального пути между двумя станциями общественного транспорта с учетом нескольких транспортных маршрутов и времени прибытия. Проект предназначен для работы со сложными транспортными сетями, включающими множество маршрутов и вариантов передачи между различными видами транспорта.

Основные функции:
1. Поиск оптимального маршрута: Пользователь может указать начальную и конечную станции, а также время отправления . Система найдет оптимальный маршрут, учитывая различные виды транспорта, время прибытия и время ожидания на пересадках.

2. Учет сложных транспортных сетей: Проект способен работать с сложными транспортными сетями, включающими автобусы и в перспективе поезда, трамваи, метро и другие виды  общественного транспорта. Он учитывает различные расписания и варианты передачи между различными видами транспорта.

3. Расширяемость: Проект разработан с учетом возможности расширения для работы с новыми видами транспорта или добавления дополнительных функций, например учет трафика или погодных условий.Для расширения модельной базы проекта, следует унаследоваться от следующих абстрактных классов: Station, Transport, Entity, ValueObject.Также в проекте Optimizer.PathMaker следует добавить генерацию путей, станций для новго транспорта.В проекте Optimizer.Console можно добавить новую функциональность по поиску пути.

Проект "Маршрутный планировщик общественного транспорта" предназначен для упрощения перемещения в городе и помощи пользователям в поиске наиболее удобного и быстрого способа достижения своего пункта назначения через общественный транспорт.




 



