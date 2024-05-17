from django import forms
from django.core.validators import MaxValueValidator, MinValueValidator

from .models import Photo, Tag


class PhotoForm(forms.ModelForm):
    photo_num = forms.ChoiceField(choices=[], required=True, label='写真番号')
    tag = forms.ModelChoiceField(queryset=Tag.objects.none(), required=True, label='Tag', widget=forms.Select(attrs={'class': 'form-control'}))

    class Meta:
        model = Photo
        fields = ('title', 'detailed_title', 'time', 'content')
        widgets = {
            'title': forms.TextInput(attrs={'class': 'form-control'}),
            'detailed_title': forms.TextInput(attrs={'class': 'form-control'}),
            'time': forms.DateInput(attrs={'class': 'form-control', 'type': 'date'}),
            'content': forms.FileInput(attrs={'class': 'form-control'}),
        }

    def __init__(self, *args, **kwargs):
        username = kwargs.pop('user', None)
        super().__init__(*args, **kwargs)
        # photo_num の選択肢を設定する
        choices = [(i, str(i)) for i in range(1, Photo.objects.count() + 2)]
        self.fields['photo_num'].choices = choices
        self.fields['tag'].queryset = Tag.objects.filter(user=username)

class TagForm(forms.ModelForm):
    class Meta:
        model = Tag
        fields = ('tag',)
        widgets = {
            'tag': forms.TextInput(attrs={'class': 'form-control'}),
        }
    def __init__(self, *args, **kwargs):
        username = kwargs.pop('user', None)  # ユーザーを取得
        super().__init__(*args, **kwargs)
        # r タグの個数を取得
        r_count = Tag.objects.filter(tag__startswith='r', user = username).count() + 1
        # s タグの個数を取得
        s_count = Tag.objects.filter(tag__startswith='s', user = username).count() + 1

        # 選択肢を動的に設定する
        choices = [('r{}'.format(r_count), 'r{}'.format(r_count))]
        choices += [('s{}'.format(s_count), 's{}'.format(s_count))]


        # tag フィールドの選択肢を更新する
        self.fields['tag'].widget = forms.Select(choices=choices, attrs={'class': 'form-control'})