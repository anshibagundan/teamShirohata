# Generated by Django 5.0.6 on 2024-05-14 03:25

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('vr_museum_app', '0009_alter_photo_content'),
    ]

    operations = [
        migrations.AddField(
            model_name='photo',
            name='photo_num',
            field=models.IntegerField(default=0),
        ),
    ]
