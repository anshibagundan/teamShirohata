package com.github.anshibagundan.vr_museum.repository;

import com.github.anshibagundan.vr_museum.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<User, Long>{
}

