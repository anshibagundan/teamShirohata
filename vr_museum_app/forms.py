from django import forms
from django.core.validators import MaxValueValidator, MinValueValidator

from .models import Photo, Tag


class PhotoForm(forms.ModelForm):
    photo_num = forms.ChoiceField(choices=[], required=True, label='写真番号')

    class Meta:
        model = Photo
        fields = ('title', 'detailed_title', 'time', 'content', 'tag')
        widgets = {
            'title': forms.TextInput(attrs={'class': 'form-control'}),
            'detailed_title': forms.TextInput(attrs={'class': 'form-control'}),
            'time': forms.DateInput(attrs={'class': 'form-control', 'type': 'date'}),
            'content': forms.FileInput(attrs={'class': 'form-control'}),
            'tag': forms.TextInput(attrs={'class': 'form-control'}),
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        # photo_num の選択肢を設定する
        choices = [(i, str(i)) for i in range(1, Photo.objects.count() + 2)]
        self.fields['photo_num'].choices = choices

class TagForm(forms.ModelForm):
    class Meta:
        model = Tag
        fields = ('tag',)
        widgets = {
            'tag': forms.TextInput(attrs={'class': 'form-control'}),
        }