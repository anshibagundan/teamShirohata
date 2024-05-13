import io
import logging
from io import BytesIO

from django.contrib import messages
from django.contrib.auth import authenticate, login
from django.contrib.auth.decorators import login_required
from django.contrib.auth.models import User
from django.core.files.uploadedfile import InMemoryUploadedFile
from django.shortcuts import redirect, render
from django.views import generic
from PIL import Image
from rest_framework import viewsets

from .models import Photo
from .serializers import PhotoSerializer

logger = logging.getLogger(__name__)

class PhotoViewSet(viewsets.ModelViewSet):
    queryset = Photo.objects.all()
    serializer_class = PhotoSerializer

def user_login(request):
    if request.method == 'POST':
        username = request.POST.get('username')
        password = request.POST.get('password')

        # ユーザー認証
        user = authenticate(request, username=username, password=password)

        if user is not None:
            # ログイン成功
            login(request, user)
            return redirect('title')
        else:
            # ログイン失敗
            messages.error(request, 'ユーザー名またはパスワードが間違っています。')
            return render(request, 'login.html')
        
    return render(request, 'login.html')

def user_create(request):
    if request.method == 'POST':
        new_username = request.POST.get('new_username')
        new_password = request.POST.get('new_password')

        try:
            # 新しいユーザーオブジェクトを作成し、ユーザー名とハッシュ化されたパスワードを設定
            user = User.objects.create_user(username=new_username, password=new_password)
            # ログメッセージ作成
            logger.info('ユーザーの作成に成功しました。ユーザー名: %s', new_username)
        except Exception as e:
            # ログメッセージ作成
            logger.error('ユーザーの作成に失敗しました。エラー: %s', e)
            messages.error(request, 'ユーザーの作成に失敗しました。エラー: {}'.format(str(e)))

        # ログイン処理
        user = authenticate(request, username=new_username, password=new_password)  # ハッシュ化されたパスワードを使用する
        if user is not None:
            messages.error(request, 'ログインに成功しました。')
            login(request, user)
            return redirect('title')
        else:
            messages.error(request, 'ユーザー名またはパスワードが間違っています。ユーザー名: {}'.format(new_password))


    return render(request, 'login_create.html')

@login_required
def title_page(request):
    if request.method == 'POST':
        title = request.POST.get('title')
        detailed_title = request.POST.get('detailed_title')
        user = request.user
        time = request.POST.get('date')
        content = request.POST.get('content')

        # content = request.FILES.get('content')
        # img = Image.open(content)
        # # モードが 'P' または 'RGBA' なら 'RGB' に変換し、透明な部分を白にする
        # messages.error(request, '画像処理前')
        # if img.mode == 'P' or img.mode == 'RGBA':
        #     # RGBAモードの画像に白い背景を設定する
        #     background = Image.new('RGB', img.size, (255, 255, 255))  # 白色の背景
        #     messages.error(request, '画像処理中')
        #     background.paste(img, (0, 0), img)  # 透明部分がある場合は、背景が見える
        #     img = background

        # # 変換後の画像を再びDjangoのInMemoryUploadedFileの形式に変換
        # byte_data = io.BytesIO()
        # img.save(byte_data, format='JPEG')
        # content = InMemoryUploadedFile(byte_data, None, content.name, 'image/jpeg', byte_data.tell(), None)

        tag = request.POST.get('tag')
        height = 0
        width = 0
        
        try:
            messages.error(request, 'インスタンス作成前')
            # Photo モデルのインスタンスを作成
            photo = Photo(title=title, detailed_title=detailed_title, user=user, time=time, content=content, height = height, width = width, tag=tag)
            messages.error(request, 'インスタンス作成あと')
            # データベースに保存
            photo.save()
            messages.error(request, 'タスクの作成に成功しました。')
        
        except Exception as e:
            # ログメッセージ作成
            logger.error('タスクの作成に失敗しました。エラー: %s', e)
            messages.error(request, 'タスクの作成に失敗しました。エラー: {}'.format(str(e)))

        return redirect('title')
    return render(request, 'index.html')

def get_photo(request):
    photo = Photo.objects.all()
    return render(request, 'photo.html', {'photos': photo})