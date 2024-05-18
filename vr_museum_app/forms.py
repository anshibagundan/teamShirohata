from django import forms
from django.core.validators import MaxValueValidator, MinValueValidator

from .models import Photo, Tag


class PhotoForm(forms.ModelForm):
    tag = forms.ModelChoiceField(
        queryset=Tag.objects.none(),
        required=True,
        label='Tag',
        widget=forms.Select(attrs={'class': 'form-control', 'id': 'id_tag', 'onchange' : 'updatePhotoNumOptions()'}),
        empty_label="--選択してください--"
    )

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
        tags = Tag.objects.filter(user=username)
        tag_choices = [('', '--選択してください--')]  # 空のチョイスを追加
        for tag in tags:
            tag_choices.append((tag.tag, tag.tag))  # 全てのタグを追加
        self.fields['tag'].choices = tag_choices

class TagForm(forms.ModelForm):
    class Meta:
        model = Tag
        fields = ('tag',)
        widgets = {
            'tag': forms.TextInput(attrs={'class': 'form-control'}),
        }

    def __init__(self, *args, **kwargs):
        username = kwargs.pop('user', None)
        super().__init__(*args, **kwargs)

        # r タグの個数を取得
        r_count = Tag.objects.filter(tag__startswith='r', user=username).count() + 1
        # s タグの個数を取得
        s_count = Tag.objects.filter(tag__startswith='s', user=username).count() + 1

        # 選択肢を動的に設定する
        choices = [
            ('r{}'.format(r_count), 'r{}'.format(r_count)),
            ('s{}'.format(s_count), 's{}'.format(s_count)),
        ]
        # tag フィールドの選択肢を更新する
        self.fields['tag'].widget = forms.Select(
            choices=[(option, option) for option in [choice[1] for choice in choices]],
            attrs={'class': 'form-control'}
        )

class TagForm_delete(forms.ModelForm):
    tag = forms.ModelChoiceField(queryset=Tag.objects.none(), required=True, label='Tag', widget=forms.Select(attrs={'class': 'form-control'}))
    class Meta:
        model = Tag
        fields = ('tag',)
        widgets = {
            'tag': forms.CheckboxSelectMultiple(),  # チェックボックスを使用する
        }
    def __init__(self, *args, **kwargs):
        username = kwargs.pop('user', None)
        super().__init__(*args, **kwargs)
        # タグの選択肢を設定する
        self.fields['tag'].queryset = Tag.objects.filter(user=username)
