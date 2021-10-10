#Uchoose main frontend project

#Architecture description and technologies
Next.js
React.js
Redux sage
bootstrap
ant design

#Key features
Application contains login page, marketplace, editor panel, user profile

#Troubleshooting
In case of issues with npm packages related to ethereumjs-abi.git or similar you could try to use the following command:
npm i eth-sig-util

#Important links
Links that could be helpful

#Important settings
Some settings for application

#Заметки. Процесс создания и сохранения NFT.
Операция создания и сохранения NFT должна быть атомарной и позволять откатывать изменения.
Малейшая ошибка на одном из этапов - остановка всех дальнейшийх действий, откат всех выполненных операций.

Процесс создания и сохранения NFT картинки.
1. Подтянуть необходимые данные для предзаполнения с backend сервера (тэги, рарки, поля, подсказки, набор картинок)
2. Заполнить данные на стороне пользователя используя часть из пункта 1 (для MVP пункт 1 опционален и можно сделать на заглушках)
3. Преобразовать в JSON и отправить данные на сохранение на backend.
4. Произвести замену base64 изображений в canvas на URL с backend (обсуждать, где и как это будет происходить)
   Предварительно. Отправляем canvas на backend в виде JSON. Извлекаем base64 изображения. Сохраняем их у нас и делаем публичными 
   Подменяем base64 на url, возвращаем JSON на front (опционально)
5. Убеждаемся, что необходимые данные сохранены на backend (опционально получаем Id операции или записи)
6. Создаем стрим с NFT коллекцией
7. Отправляем данные о стриме и ID из пункта 5 (для установления маппинга и сохранения) на backend.
8. Говорим, что NFT картинка создана успешно.

Ошибки в пунктах 4-7 должны привести к откату изменений на backend. Откатить изменения в NFT сети мы не можем. Очень опасное место


Заменить пункт 4 на обращение сразу в сеть для создания стрима мы не можем, т.к. в canvas содержатся данные о base64. Их нужно заменить реальными url

