from rest_framework import serializers

from .models import Photo


class PhotoSerializer(serializers.ModelSerializer):
    class Meta:
        model = Photo
        fields = ['id', 'title', 'detailed_title', 'user', 'time', 'content', 'height', 'width', 'tag']