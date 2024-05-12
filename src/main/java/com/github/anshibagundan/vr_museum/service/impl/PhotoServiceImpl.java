package com.github.anshibagundan.vr_museum.service.impl;

import com.github.anshibagundan.vr_museum.dto.PhotoDto;
import com.github.anshibagundan.vr_museum.entity.Photo;
import com.github.anshibagundan.vr_museum.exception.ResourceNotFoundException;
import com.github.anshibagundan.vr_museum.mapper.PhotoMapper;
import com.github.anshibagundan.vr_museum.repository.PhotoRepository;
import com.github.anshibagundan.vr_museum.repository.UserRepository;
import com.github.anshibagundan.vr_museum.service.PhotoService;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;


@Service
@AllArgsConstructor
public class PhotoServiceImpl implements PhotoService {

    private final UserRepository userRepository;
    private PhotoRepository photoRepository;

    @Override
    public PhotoDto createPhoto(PhotoDto photoDto) {

        Photo photo = PhotoMapper.mapToPhoto(photoDto);
        Photo savedPhoto = photoRepository.save(photo);
        return PhotoMapper.mapToPhotoDto(savedPhoto);
    }

    @Override
    public PhotoDto getPhotoById(Long PhotoId) {
        Photo photo = photoRepository.findById(PhotoId)
                .orElseThrow(() ->
                        new ResourceNotFoundException("Photo is not exists with given id : " + PhotoId));
        return PhotoMapper.mapToPhotoDto(photo);
    }

    @Override
    public List<PhotoDto> getAllPhotos() {
        List<Photo> photos = photoRepository.findAll();
        return photos.stream().map((photo) -> PhotoMapper.mapToPhotoDto(photo))
                .collect(Collectors.toList());
    }

    @Override
    public PhotoDto updatePhoto(Long photoId, PhotoDto updatedPhoto) {

        Photo photo = photoRepository.findById(photoId).orElseThrow(
                () -> new ResourceNotFoundException("User is not exists with given id :" + photoId)
        );

        photo.setTitle(updatedPhoto.getTitle());
        photo.setDescription(updatedPhoto.getDescription());
        photo.setImg(updatedPhoto.getImg());
        photo.setUser(updatedPhoto.getUser());
        photo.setTime(updatedPhoto.getTime());
        photo.setTag(updatedPhoto.getTag());

        Photo updatedPhotoObj = photoRepository.save(photo);

        return PhotoMapper.mapToPhotoDto(updatedPhotoObj);
    }

    @Override
    public void deletePhoto(Long photoId) {
        Photo photo = photoRepository.findById(photoId).orElseThrow(
                () -> new ResourceNotFoundException("User is not exists with given id :" + photoId)
        );

        photoRepository.delete(photo);
    }

}
