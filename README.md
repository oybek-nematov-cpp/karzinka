# 🛒 Product Details API (Karzinka)

Bu loyiha **ASP.NET Core Web API** va **Dapper** yordamida yozilgan bo‘lib, `karzinka` nomli PostgreSQL bazasidagi `product` va `product_details` jadvallari bilan ishlaydi.

API orqali mahsulotlar haqida ma’lumotlarni qo‘shish, yangilash, o‘chirish va ko‘rish mumkin.

![photo_2025-07-24_17-18-37](https://github.com/user-attachments/assets/ad52f145-9833-4ab1-8e23-077db7141ec6)


---

## 📦 Jadval va Model

### 🔹 Jadval: `product_details`

PostgreSQL'da quyidagicha yaratiladi:

```sql
CREATE TABLE product_details (
    id INT PRIMARY KEY,
    product_id INT,
    product TEXT,
    unit_price NUMERIC,
    quantity INT
);
```
