package com.github.anshibagundan.vr_museum.repository;

import com.github.anshibagundan.vr_museum.entity.Photo;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PhotoRepository extends JpaRepository<Photo, Long> {
}
