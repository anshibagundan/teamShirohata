package com.github.anshibagundan.vr_museum.controller;


import com.github.anshibagundan.vr_museum.dto.PhotoDto;
import com.github.anshibagundan.vr_museum.service.PhotoService;
import lombok.AllArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@AllArgsConstructor
@RestController
@RequestMapping("/api/photo")
public class PhotoController {

    private PhotoService photoService;

    @PostMapping
    public ResponseEntity<PhotoDto> createPhoto(@RequestBody PhotoDto photoDto) {
        PhotoDto savedPhoto = photoService.createPhoto(photoDto);
        return new ResponseEntity<>(savedPhoto, HttpStatus.CREATED);
    }

    @GetMapping("{id}")
    public ResponseEntity<PhotoDto> getPhotoById(@PathVariable Long photoId) {
        PhotoDto photoDto = photoService.getPhotoById(photoId);
        return ResponseEntity.ok(photoDto);
    }

    @GetMapping
    public ResponseEntity<List<PhotoDto>> getAllPhotos() {
        List<PhotoDto> photos = photoService.getAllPhotos();
        return ResponseEntity.ok(photos);
    }

    @PutMapping("{id}")
    public ResponseEntity<PhotoDto> updatePhoto(@PathVariable("id") Long PhotosId, @RequestBody PhotoDto updatedPhoto) {
        PhotoDto photoDto = photoService.updatePhoto(PhotosId, updatedPhoto);
        return ResponseEntity.ok(photoDto);
    }

    @DeleteMapping("{id}")
    public ResponseEntity<String> deletePhoto(@PathVariable("id") Long PhotosId) {
        photoService.deletePhoto(PhotosId);
        return ResponseEntity.ok("Photo deleted successfully");
    }
}
