# Бронирование времени для автомойки. Попытка в микросервисы

* [О проекте](#AboutProject)

<a id="AboutProject"></a>

## О проекте
Разделено на три микросервиса: бронирование, услуги и расписание(не выделили, но можно вынести). 
Сделано на бэке все очень так себе по ему мнению, пропущены многие проверки и тд.

- моментальное оповещения - SignalR
- для дальнейшего CI/CD - docker
- onion архитектура

Надо было везде юзать DTO, нормалньо сделаь мапперы, хотелось в DDD и почекать CQRS. Но вышло, что вышло. Делалось очень быстро


### Фоточки:
#### Спроектированная диаграмма классов для брони
![Спроектированная диаграмма классов для брони](images/5.jpg)
#### Выбор организация/пользователь
![Выбор организация/пользователь](images/1.jpg)
#### Выбор услуги и доступного ремени организации пользователем
![Выбор услуги и доступного ремени организации пользователем](images/2.jpg)
#### Моментальное оповещение организации
![Моментальное оповещение организации](images/3.jpg)
#### Моментальное оповещение пользователя
![Моментальное оповещение пользователя](images/4.jpg)	


