from django.db import models


class Post(models.Model):
    title = models.CharField(max_length=40)
    detailed_title = models.CharField(max_length=100)
    user = models.CharField(max_length=20)
    time = models.DateTimeField()
    content = models.TextField()
    height = models.IntegerField()
    width = models.IntegerField()
    tag = models.CharField(max_length=20)

    def __str__(self):
        return self.title
